using Dapper;
using MegaHack.Core.Models.Output;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Linq;

namespace MegaHack.Core.Repository
{
    public class ProdutoRepository : BaseRepository
    {
        public ProdutoRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<List<ProdutoOutput>> BuscarProdutoPorDescricao(string Descricao)
        {
            using (var conn = base.GetConnection())
            {
                conn.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Descricao", Descricao);

                var retorno = await conn.QueryAsync(sql: @"SELECT ID_Produto
                                                                 ,Descricao
                                                                 ,Quantidade
                                                                 ,Preco
                                                                 ,Imagem
                                                           FROM Produto WITH (NOLOCK)
                                                           WHERE Descricao LIKE  '%' + UPPER(@Descricao) + '%'",
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
