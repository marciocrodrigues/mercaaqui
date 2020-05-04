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
    public class EntregadorRepository : BaseRepository
    {
        public EntregadorRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<List<EntregadorOutput>> ListarEntregadores()
        {
            using(var conn = base.GetConnection())
            {
                conn.Open();

                var retorno = await conn.QueryAsync(sql: @"SELECT ID_Entregador
                                                                 ,Nome
	                                                           	 ,Logradouro
	                                                           	 ,Numero
	                                                           	 ,Cep
	                                                           	 ,Cidade
	                                                           	 ,Estado
	                                                           	 ,DDD
	                                                           	 ,Telefone
                                                                 ,Imagem
                                                           FROM fn_ListarEntregadores()",
                                                     commandType: CommandType.Text);

                List<EntregadorOutput> lista = new List<EntregadorOutput>();

                lista.AddRange(retorno.ToList().Select(x =>
                {
                    return new EntregadorOutput
                    {
                        ID_Entregador = x?.ID_Entregador,
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

        public async Task<EntregadorOutput> BuscarEntregadorPorId(int ID_Entregador)
        {
            using(var conn = base.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID_Entregador", ID_Entregador);

                var retorno = await conn.QueryFirstAsync(sql: @"SELECT ID_Entregador
                                                                       ,Nome
                                                                 	   ,Logradouro
                                                                 	   ,Numero
                                                                 	   ,Cep
                                                                 	   ,Cidade
                                                                 	   ,Estado
                                                                 	   ,DDD
                                                                 	   ,Telefone
                                                                       ,Imagem
                                                                 FROM Entregador WITH (NOLOCK)
                                                                      INNER JOIN Pessoa ON Pessoa.ID_Pessoa = Entregador.ID_Pessoa
                                                                      INNER JOIN Endereco ON Endereco.ID_Endereco = Entregador.ID_Endereco
                                                           	          INNER JOIN Contato ON Contato.ID_Contato = Entregador.ID_Contato
                                                                 WHERE ID_Entregador = @ID_Entregador",
                                                           param: parameters,
                                                           commandType: CommandType.Text);
                return new EntregadorOutput
                {
                    ID_Entregador = retorno?.ID_Entregador,
                    Nome = retorno?.Nome,
                    Logradouro = retorno?.Logradouro,
                    Numero = retorno?.Numero,
                    Cep = retorno?.Cep,
                    Cidade = retorno?.Cidade,
                    Estado = retorno?.Estado,
                    DDD = retorno?.DDD,
                    Telefone = retorno?.Telefone,
                    Imagem = retorno?.Imagem
                };
            }
        }
    }
}
