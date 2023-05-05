using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Demo.WebApplication.DL.DBConfig
{
    public interface IStorage
    {
        /// <summary>
        /// Kết nối đến DB và mở kết nối
        /// </summary>
        /// <returns>kết nối mới đến DB</returns>
         IDbConnection GetDbConnection();

        /// <summary>
        /// Thực thi câu lệnh truy vấn
        /// </summary>
        /// <returns>trả về số lượng bản ghi bị ảnh hưởng</returns>
         int Execute(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        T ExecuteScalar<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);


        /// <summary>
        /// Thực hiện câu lệnh truy vấn
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        IEnumerable<T> Query<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Thực hiện câu lệnh truy vấn trả về nhiều danh sách bản ghi
        /// </summary>
        /// <returns>Trả về nhiều danh sách bản ghi</returns>
        GridReader QueryMultiple(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Thực thi câu truy vấn và lấy ra bản ghi đầu tiên
        /// </summary>
        /// <returns>Lấy bản ghi đầu tiên</returns>
        T QueryFirstOrDefault<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Thực hiện 1 giao dịch
        /// </summary>
        /// <returns></returns>
        IDbTransaction GetDbTransaction(IDbConnection cnn);

        /// <summary>
        /// Hoàn thành 1 giao dịch 
        /// </summary>
        void Commit(IDbTransaction transaction);

        /// <summary>
        /// Phục hồi 1 giao dịch
        /// </summary>
        void Rollback(IDbTransaction transaction);




    }
}
