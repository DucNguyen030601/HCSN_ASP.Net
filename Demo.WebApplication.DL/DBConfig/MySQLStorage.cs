using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Demo.WebApplication.DL.DBConfig
{
    public class MySQLStorage : IStorage
    {
        #region Field

        private readonly string _connectionString = DatabaseContext.ConnectionString;

        #endregion

        #region Method 

        /// <summary>
        /// Kết nối đến DB và mở kết nối
        /// </summary>
        /// <returns>kết nối mới đến DB</returns>
        public IDbConnection GetDbConnection()
        {
            var mySqlConnection =  new MySqlConnection(_connectionString);
            mySqlConnection.Open();
            return mySqlConnection;
        }

        /// <summary>
        /// Thực thi câu lệnh truy vấn
        /// </summary>
        /// <returns>trả về số lượng bản ghi bị ảnh hưởng</returns>
        public int Execute(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Execute(sql, param, transaction, commandTimeout, commandType);
        }
        /// <summary>
        /// Thực thi câu lệnh truy vấn
        /// </summary>
        /// <returns>trả về số lượng bản ghi bị ảnh hưởng</returns>
        public T ExecuteScalar<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Thực hiện câu lệnh truy vấn
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public IEnumerable<T> Query<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// Thực hiện câu lệnh truy vấn trả về nhiều danh sách bản ghi
        /// </summary>
        /// <returns>Trả về nhiều danh sách bản ghi</returns>
        public GridReader QueryMultiple(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Thực thi câu truy vấn và lấy ra bản ghi đầu tiên
        /// </summary>
        /// <returns>Lấy bản ghi đầu tiên</returns>
        public T QueryFirstOrDefault<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Thực hiện 1 giao dịch
        /// </summary>
        /// <returns></returns>
        public IDbTransaction GetDbTransaction(IDbConnection cnn)
        {
            return cnn.BeginTransaction();
        }

        /// <summary>
        /// Hoàn thành 1 giao dịch 
        /// </summary>
        public void Commit(IDbTransaction transaction)
        {
            transaction.Commit();
        }

        /// <summary>
        /// Phục hồi 1 giao dịch
        /// </summary>
        public void Rollback(IDbTransaction transaction)
        {
            transaction.Rollback();
        }

        #endregion

    }
}

