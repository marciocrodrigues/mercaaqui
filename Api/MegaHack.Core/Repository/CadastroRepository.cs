using Dapper;
using MegaHack.Core.Models.Input;
using MegaHack.Core.Models.Output;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MegaHack.Core.Repository
{
    public class CadastroRepository : BaseRepository
    {
        public CadastroRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<CadastroOutput> Cadastrar(CadastroInput cadastro)
        {
            using(var conn = base.GetConnection())
            {
                conn.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Nome", cadastro.Nome);
                parameters.Add("@Nome_Fantasia", cadastro.Nome_Fantasia);
                parameters.Add("@Documento", cadastro.Documento);
                parameters.Add("@Email", cadastro.Email);
                parameters.Add("@Senha", cadastro.Senha);
                parameters.Add("@Logradouro", cadastro.Logradouro);
                parameters.Add("@Numero", cadastro.Numero);
                parameters.Add("@Bairro", cadastro.Bairro);
                parameters.Add("@Cep", cadastro.Cep);
                parameters.Add("@Cidade", cadastro.Cidade);
                parameters.Add("@Estado", cadastro.Estado);
                parameters.Add("@DDD", cadastro.DDD);
                parameters.Add("@Telefone", cadastro.Telefone);
                parameters.Add("@Tipo", cadastro.Tipo);
                parameters.Add("@Imagem", cadastro.Imagem);
                parameters.Add("@ID_Identificador", 0, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@Return_Code", 0, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@ErrMsg", string.Empty, DbType.String, ParameterDirection.Output);

                await conn.ExecuteAsync(sql: "st_ProcCadastrar",
                                        param: parameters,
                                        commandType: CommandType.StoredProcedure);

                if(parameters.Get<int>("@Return_Code") > 0)
                {
                    return new CadastroOutput
                    {
                        ID_Identificador = parameters.Get<int>("@ID_Identificador"),
                        Return_Code = parameters.Get<int>("@Return_Code"),
                        ErrMsg = parameters.Get<string>("@ErrMsg")
                    };
                }

                return new CadastroOutput
                {
                    ID_Identificador = parameters.Get<int>("@ID_Identificador"),
                    Return_Code = 0,
                    ErrMsg = string.Empty
                };
            }
        }

        public async Task<ProdutoOutput> CadastrarProduto(ProdutoInput produto)
        {
            using (var conn = base.GetConnection())
            {
                conn.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID_Produto", produto.ID_Produto);
                parameters.Add("@ID_Comerciante", produto.ID_Comerciante);
                parameters.Add("@Descricao", produto.Descricao);
                parameters.Add("@Quantidade", produto.Quantidade);
                parameters.Add("@Preco", produto.Preco);
                parameters.Add("@Imagem", produto.Imagem);
                parameters.Add("@ID_Identificador", 0, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@Return_Code", 0, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@ErrMsg", 0, DbType.String, ParameterDirection.Output);

                await conn.ExecuteAsync(sql: "st_ProcProdutoAdicionarAtualizar",
                                        param: parameters,
                                        commandType: CommandType.StoredProcedure);

                if(parameters.Get<int>("@Return_Code") > 0)
                {
                    return new ProdutoOutput
                    {
                        ID_Produto = 0,
                        Return_Code = parameters.Get<int>("@Return_Code"),
                        ErrMsg = parameters.Get<string>("@ErrMsg"),
                        Descricao = string.Empty,
                        Quantidade = 0,
                        Preco = 0,
                        Imagem = string.Empty
                    };
                }

                return new ProdutoOutput
                {
                    ID_Produto = parameters.Get<int>("@ID_Identificador"),
                    Descricao = produto.Descricao,
                    Quantidade = produto.Quantidade,
                    Preco = produto.Preco,
                    Imagem = produto.Imagem,
                    Return_Code = 0,
                    ErrMsg = string.Empty
                };
            }
        }

        public async Task<ProcessoOutput> CadastrarProcesso(ProcessoInput processo)
        {
            using(var conn = base.GetConnection())
            {
                conn.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID_Processo", processo.ID_Processo);
                parameters.Add("@No_Comprovante", processo.NO_Comprovante);
                parameters.Add("@Data_Atualizacao", processo.Data_Atualizacao);
                parameters.Add("@Data_Finalizacao", processo.Data_Finalizacao);
                parameters.Add("@Status", processo.Status);
                parameters.Add("@ID_Comerciante", processo.ID_Comerciante);
                parameters.Add("@ID_Cliente", processo.ID_Cliente);
                parameters.Add("@ID_Produto", processo.ID_Produto);
                parameters.Add("@ID_Entregador", processo.ID_Entregador);
                parameters.Add("@Quantidade", processo.Quantidade);
                parameters.Add("@ID_Identificador", 0, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@Return_Code", 0, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@ErrMsg", string.Empty, DbType.String, ParameterDirection.Output);

                await conn.ExecuteAsync(sql: "st_ProcProcessoAdicionarAtualizar",
                                        param: parameters,
                                        commandType: CommandType.StoredProcedure);

                if(parameters.Get<int>("@Return_Code") > 0)
                {
                    return new ProcessoOutput
                    {
                        ID_Processo = 0,
                        ID_Comerciante = 0,
                        ID_Cliente = 0,
                        ID_Entregador = 0,
                        ID_Produto = 0,
                        Return_Code = parameters.Get<int>("@Return_Code"),
                        ErrMsg = parameters.Get<string>("@errMsg")
                    };
                }

                return new ProcessoOutput
                {
                    ID_Processo = parameters.Get<int>("@ID_Identificador"),
                    Data_Atualizacao = processo.Data_Atualizacao,
                    Data_Finalizacao = processo.Data_Finalizacao,
                    ID_Comerciante = processo.ID_Comerciante,
                    ID_Cliente = processo.ID_Cliente,
                    ID_Produto = processo.ID_Produto,
                    ID_Entregador = processo.ID_Entregador,
                    Return_Code = 0,
                    ErrMsg = string.Empty
                };
            }
        }
    }
}
