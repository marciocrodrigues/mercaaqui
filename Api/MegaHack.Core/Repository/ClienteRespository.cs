using Dapper;
using MegaHack.Core.Models.Output;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaHack.Core.Repository
{
    public class ClienteRespository : BaseRepository
    {
        public ClienteRespository(string connectionString) : base(connectionString)
        {

        }

        public async Task<List<ClienteOutput>> ListarClientes()
        {
            using (var conn = base.GetConnection())
            {
                conn.Open();

                var retorno = await conn.QueryAsync(sql: @"SELECT ID_Cliente
                                                                 ,Nome
	                                                           	 ,Logradouro
	                                                           	 ,Numero
	                                                           	 ,Cep
	                                                           	 ,Cidade
	                                                           	 ,Estado
	                                                           	 ,DDD
	                                                           	 ,Telefone
                                                                 ,Imagem
                                                           FROM fn_ListarClientes()",
                                                     commandType: CommandType.Text);

                List<ClienteOutput> lista = new List<ClienteOutput>();

                lista.AddRange(retorno.ToList().Select(x =>
                {
                    return new ClienteOutput
                    {
                        ID_Cliente = x?.ID_Cliente,
                        Nome = x?.Nome,
                        Logradouro = x?.Logradouro,
                        Numero = x?.Numero,
                        Cep = x?.Cep,
                        Cidade = x?.Cidade,
                        Estado = x?.Estado,
                        DDD = x?.DDD,
                        Telefone = x?.Telefone,
                        Imagem = x?.Imagem
                    };
                }));

                return lista;
            }
        }

        public async Task<ClienteOutput> BuscarEntregadorPorId(int ID_Cliente)
        {
            using (var conn = base.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID_Cliente", ID_Cliente);

                var retorno = await conn.QueryFirstAsync(sql: @"SELECT ID_Cliente
                                                                     ,Nome
                                                               	     ,Logradouro
                                                               	     ,Numero
                                                               	     ,Cep
                                                               	     ,Cidade
                                                               	     ,Estado
                                                               	     ,DDD
                                                               	     ,Telefone
                                                                     ,Imagem
                                                               FROM Cliente WITH (NOLOCK)
                                                                    INNER JOIN Pessoa ON Pessoa.ID_Pessoa = Cliente.ID_Pessoa
                                                                    INNER JOIN Endereco ON Endereco.ID_Endereco = Cliente.ID_Endereco
                                                        	           INNER JOIN Contato ON Contato.ID_Contato = Cliente.ID_Contato
                                                               WHERE ID_Cliente = @ID_Cliente",
                                                        param: parameters,
                                                        commandType: CommandType.Text);
                return new ClienteOutput
                {
                    ID_Cliente = retorno?.ID_Cliente,
                    Nome = retorno?.Nome,
                    Logradouro = retorno?.Logradouro,
                    Numero = retorno?.Numero,
                    Cep = retorno?.Cep,
                    Cidade = retorno?.Cidade,
                    Estado = retorno?.Estado,
                    DDD = retorno?.DDD,
                    Telefone = retorno?.Telefone,
                    Imagem = retorno?.Image
                };
            }
        }
    }
}
