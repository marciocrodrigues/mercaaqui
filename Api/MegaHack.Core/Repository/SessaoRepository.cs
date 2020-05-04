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
    public class SessaoRepository : BaseRepository
    {
        public SessaoRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<SessaoOuput> IniciarSessao(SessaoInput param)
        {
            using(var conn = base.GetConnection())
            {
                conn.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Email", param.Email);
                parameter.Add("@Senha", param.Senha);
                parameter.Add("@ID_Identificador", 0, DbType.Int32, ParameterDirection.Output);
                parameter.Add("@Tipo", string.Empty, DbType.String, ParameterDirection.Output);
                parameter.Add("@Return_Code", 0, DbType.Int32, ParameterDirection.Output);
                parameter.Add("@ErrMsg", string.Empty, DbType.String, ParameterDirection.Output);

                await conn.ExecuteAsync(sql: "st_Login",
                                        param: parameter,
                                        commandType: CommandType.StoredProcedure);

                if(parameter.Get<int>("@Return_Code") > 0)
                {
                    return new SessaoOuput
                    {
                        ID_Identificador = 0,
                        Tipo = string.Empty,
                        Return_Code = parameter.Get<int>("@Return_Code"),
                        ErrMsg = parameter.Get<string>("@ErrMsg")
                    };
                }

                return new SessaoOuput
                {
                    ID_Identificador = parameter.Get<int>("@ID_Identificador"),
                    Tipo = parameter.Get<string>("@Tipo"),
                    Return_Code = 0,
                    ErrMsg = string.Empty
                };

            }
        }
    }
}
