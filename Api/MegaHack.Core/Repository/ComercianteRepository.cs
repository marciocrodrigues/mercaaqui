using Dapper;
using MegaHack.Core.Models.Input;
using MegaHack.Core.Models.Output;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaHack.Core.Repository
{
    public class ComercianteRepository : BaseRepository
    {
        public ComercianteRepository(string connectionString): base(connectionString)
        {

        }

        public async Task<List<ComercianteOutput>> ComerciantePorCidadeEstado(ComercianteInput param)
        {
            using(var conn = base.GetConnection())
            {
                conn.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Cidade", param.Cidade);
                parameters.Add("@Estado", param.Estado);

                var retorno = await conn.QueryAsync(sql: @"SELECT ID_Comerciante
                                                                 ,Nome
                                                           	     ,Nome_Fantasia
                                                           	     ,Logradouro
                                                           	     ,Numero
                                                           	     ,Cep
                                                           	     ,Cidade
                                                           	     ,Estado
                                                           	     ,DDD
                                                           	     ,Telefone
                                                                 ,Imagem
                                                           FROM fn_BuscaVendedores(@Cidade, @Estado)",
                                                     param: parameters,
                                                     commandType: CommandType.Text);

                List<ComercianteOutput> lista = new List<ComercianteOutput>();

                lista.AddRange(retorno.ToList().Select(p =>
                {
                    var comerciante = new ComercianteOutput();
                    comerciante.ID_Comerciante = p?.ID_Comerciante;
                    comerciante.Nome = p?.Nome;
                    comerciante.Nome_Fantasia = p?.Nome_Fantasia;
                    comerciante.Logradouro = p?.Logradouro;
                    comerciante.Numero = p?.Numero;
                    comerciante.Cep = p?.Cep;
                    comerciante.Cidade = p?.Cidade;
                    comerciante.Estado = p?.Estado;
                    comerciante.DDD = p?.DDD;
                    comerciante.Telefone = p?.Telefone;
                    comerciante.Imagem = p?.Imagem;

                    return comerciante;
                }));

                return lista;
            }
        }

        public async Task<ComercianteOutput> BuscarComerciantePorId(int ID_Comerciante)
        {
            using (var conn = base.GetConnection())
            {
                conn.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID_Comerciante", ID_Comerciante);

                var retorno = await conn.QueryFirstAsync(sql: @"SELECT ID_Comerciante
                                                                          ,Nome
                                                                    	  ,Nome_Fantasia
                                                                    	  ,Logradouro
                                                                    	  ,Numero
                                                                    	  ,Cep
                                                                    	  ,Cidade
                                                                    	  ,Estado
                                                                    	  ,DDD
                                                                    	  ,Telefone
                                                                    	  ,Imagem
                                                                    FROM Comerciante
                                                                         INNER JOIN Pessoa ON Pessoa.ID_Pessoa = Comerciante.ID_Pessoa
                                                                         INNER JOIN Endereco ON Endereco.ID_Endereco = Comerciante.ID_Endereco
                                                                         INNER JOIN Contato ON Contato.ID_Contato = Comerciante.ID_Contato
                                                                    WHERE ID_Comerciante = @ID_Comerciante",
                                                      param: parameters,
                                                      commandType: CommandType.Text);

                return new ComercianteOutput
                {
                    ID_Comerciante = retorno?.ID_Comerciante,
                    Nome = retorno?.Nome,
                    Nome_Fantasia = retorno?.Nome_Fantasia,
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

        public async Task<List<ProdutoOutput>> BuscarProdutoPorComerciante(int ID_Comerciante)
        {
            using (var conn = base.GetConnection())
            {
                conn.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID_Comerciante", ID_Comerciante);

                var retorno = await conn.QueryAsync(sql: @"SELECT ID_Produto
                                                                 ,Descricao
		                                                         ,Quantidade
		                                                         ,Preco
                                                                 ,Imagem
                                                           FROM fn_BuscaProdutosComerciantes(@ID_Comerciante)",
                                                    param: parameters,
                                                    commandType: CommandType.Text);

                List<ProdutoOutput> lista = new List<ProdutoOutput>();

                lista.AddRange(retorno.ToList().Select(x =>
                {
                    return new ProdutoOutput
                    {
                        ID_Produto = x?.ID_Produto,
                        Descricao = x?.Descricao,
                        Quantidade = x?.Quantidade,
                        Preco = x?.Preco,
                        Imagem = x?.Imagem
                    };
                }));

                return lista;
            }
        }
    }
}
