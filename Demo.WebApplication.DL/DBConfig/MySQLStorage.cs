using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.DL.DBConfig
{
    public class MySQLStorage : IStorage
    {
        #region Field

        private readonly string _connectionString = "server=localhost;database=misa.qlts_mf1566_nnduc;uid=root;pwd=;Old Guids=true;";

        #endregion

        #region Method 
        public IDbConnection GetDbConnection()
        {
            var mySqlConnection =  new MySqlConnection(_connectionString);
            mySqlConnection.Open();
            return mySqlConnection;
        }

        public int Execute(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        public  IEnumerable<T> Query<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        #endregion

    }
}
