using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MegaHack.Core.Repository
{
    public class BaseRepository
    {
        private string _connectionString;
        private SqlConnection _conexao;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            _conexao = new SqlConnection(_connectionString);
            return _conexao;
        }
    }
}
