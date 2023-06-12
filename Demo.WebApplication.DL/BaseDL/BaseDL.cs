using Dapper;
using Demo.WebApplication.Common;
using Demo.WebApplication.Common.Constants;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Enums;
using Demo.WebApplication.DL.DBConfig;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region Field

        protected IStorage _storage;
        protected string className = Functions.ConvertPascalCaseToUnderscore(typeof(T).Name);

        #endregion

        #region Contructor

        public BaseDL(IStorage storage)
        {
            _storage = storage;
        }

        #endregion

        /// <summary>
        /// Hàm thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created by: NNduc(21/03/2023)
        public int InsertRecord(T record)
        {
            //Chuẩn bị tham số đầu vào cho stored
            string storedProcedureName = string.Format(StoredProcedures.Insert, className);

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                parameters.Add($"@{property.Name}", property.GetValue(record));
            }

            //Số lượng bản ghi bị ảnh hưởng
            int numberAffectedRows;

            //Kết nối đến ĐB
            using (var dbConnection = _storage.GetDbConnection())
            {
                //Gọi vào ĐB
                numberAffectedRows = _storage.Execute(dbConnection, storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return numberAffectedRows;
        }

        /// <summary>
        /// Kiểm tra mã tồn tại hay chưa
        /// </summary>
        /// <param name="code">Mã muốn kiểm tra</param>
        /// <param name="state">Trạng thái của </param>
        /// <returns>
        /// True -> có tồn tại
        /// False -> không tồn tại 
        /// </returns>
        /// Created by: NNduc(21/03/2023)
        public bool IsCodeExist(string code, string? id = "")
        {
            int numberCodeExits = 0;
            var storedProcedure = StoredProcedures.DuplicateCode;
            var parameters = new DynamicParameters();
            parameters.Add("@table", className);
            parameters.Add("@code", code);
            parameters.Add("@id", id);

            //kết nối đến DB
            using (var dbConnection = _storage.GetDbConnection())
            {
                numberCodeExits = _storage.QueryFirstOrDefault<int>(dbConnection, storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
            return numberCodeExits == 1;
        }

        /// <summary>
        /// Hàm sửa 1 bản ghi
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1: Nếu update thành công
        /// 0: Nếu delete thất bại
        /// </returns>
        /// Created by: NNduc(21/03/2023)
        public int UpdateRecord(string id, T record)
        {
            //Chuẩn bị stored
            string storedProcedureName = string.Format(StoredProcedures.Update, className);

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            //Lấy tất cả tên properties trong bản ghi
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == $"{className}_id") continue; //không thêm parameter nếu property là id
                parameters.Add($"@{property.Name}", property.GetValue(record));
            }

            //Thêm parameter id
            parameters.Add($"@{className}_id", id);

            //Số lượng bản ghi bị ảnh hưởng
            int numberAffectedRows;

            //Kết nối đến ĐB
            using (var dbConnection = _storage.GetDbConnection())
            {
                //Gọi vào ĐB
                numberAffectedRows = _storage.Execute(dbConnection, storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return numberAffectedRows;
        }

        /// <summary>
        /// Hàm xoá 1 bản ghi
        /// </summary>
        /// <param name="entityId">Mã id của thực thể cần xoá</param>
        /// <returns>
        /// Số bản ghi bị ảnh hưởng
        /// </returns>
        /// Created by: NNduc(21/03/2023)
        public virtual int DeleteRecord(string entityId)
        {
            //Chuẩn bị tham số đầu vào cho stored
            string storedProcedureName = string.Format(StoredProcedures.Delete, className);

            //truyền tham số vào
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{className}_id", entityId);

            //Số lượng bản ghi bị ảnh hưởng
            var numberAffectedRow = 0;

            //Kết nối đến DB
            using (var dbConnection = _storage.GetDbConnection())
            {
                numberAffectedRow = _storage.Execute(dbConnection, storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return numberAffectedRow;
        }

        /// <summary>
        /// Hàm xoá nhiều bản ghi
        /// </summary>
        /// <param name="entityIds">Danh sách ID muốn xoá</param>
        /// <returns>
        /// true: Nếu xoá thành công
        /// false: Nếu xoá thất bại
        /// </returns>
        /// Author NNduc(23-03-29)
        public virtual bool DeleteRecords(List<string> entityIds)
        {
            string entityIdString = "'" + string.Join("','", entityIds) + "'";
            int countRecord = entityIds.Count;//số lượng id
            int numberAffectedRow;
            //Chuẩn bị tham số đầu vào cho stored
            string storedProcedureName = string.Format(StoredProcedures.DeleteMultiple, className);

            //truyền tham số vào
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{className}_ids", entityIdString);

            using (var dbConnection = _storage.GetDbConnection())
            {
                using (var dbTrancation = _storage.GetDbTransaction(dbConnection))
                {
                    try
                    {
                        numberAffectedRow = _storage.Execute(dbConnection, storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                        //nếu số lượng bản ghi ảnh hưởng khác số lượng id
                        throw new NotImplementedException();
                        if (numberAffectedRow != countRecord) { _storage.Rollback(dbTrancation); return false; }
                        _storage.Commit(dbTrancation);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _storage.Rollback(dbTrancation);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Hàm lấy ra mã code mới
        /// </summary>
        /// <returns>Mã code cần lấy</returns>
        /// Created by: NNduc(21/03/2023)
        public string GetNewCode()
        {
            string storedProcedureName = StoredProcedures.GetNewCode;
            var paramters = new DynamicParameters();
            paramters.Add("@table", className);
            string newCode;
            //Kết nối đến ĐB
            using (var dbConnection = _storage.GetDbConnection())
            {
                //Gọi vào ĐB
                newCode = _storage.QueryFirstOrDefault<string>(dbConnection, storedProcedureName, paramters, commandType: CommandType.StoredProcedure);
            }
            return Functions.GetNewCode(newCode);
        }

        /// <summary>
        /// Lấy danh sách tất cả các bản ghi
        /// </summary>
        /// <returns>Danh sách</returns>
        /// Created by: NNduc(21/03/2023)
        public List<T> GetRecords()
        {
            //chuẩn bị tên view
            string view = string.Format(Tables.View, className);

            //danh sách bản ghi
            List<T> values = new List<T>();

            //kết nối đến DB
            using (var dbConnection = _storage.GetDbConnection())
            {
                values = (List<T>)_storage.Query<T>(dbConnection, view, commandType: CommandType.TableDirect);
            }
            return values;
        }

        //Số lượng bản ghi bị trùng code
        /*   private int NumberCodeExits(string code)
           {
               //chuẩn bị stored procedure
               string storedProcedureName = StoredProcedures.DuplicateCode;

               //Chuẩn bị tham số
               var parameters = new DynamicParameters();
               parameters.Add("@table", className);
               parameters.Add("@code", code);

               //Số lượng bản ghi trùng
               int numberCodeExits;

               //kết nối đến DB
               using (var dbConnection = _storage.GetDbConnection())
               {
                   numberCodeExits = _storage.QueryFirstOrDefault<int>(dbConnection, storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
               }
               return numberCodeExits;
           }*/

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id">id bản ghi muốn lấy</param>
        /// <returns>Bản ghi thoả mãn điều kiện</returns>
        /// Created by: NNduc(21/03/2023)
        public T GetRecordsById(string id)
        {
            string storedProcedureName = string.Format(StoredProcedures.GetById);
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@table", className);

            T record;

            //Kết nối đến ĐB
            using (var dbConnection = _storage.GetDbConnection())
            {
                //Gọi vào ĐB
                record = _storage.QueryFirstOrDefault<T>(dbConnection, storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return record;
        }

        /// <summary>
        /// Lấy danh sách bản ghi và tổng số bản ghi có phân trang
        /// </summary>
        /// <param name="filter">bộ lọc tìm theo tên, hoặc mã</param>
        /// <param name="page">trang hiện tại</param>
        /// <param name="pageSize">số lượng bản ghi trong 1 trang</param>
        /// <param name="sort">sắp xếp</param>
        /// <returns>Trả về thông tin danh sách bản ghi và tổng số bản ghi có phân trang</returns>
        /// Created by: NNduc(21/03/2023)
        public PagingResult<P> GetPagingResult<P>(int? page = 1, int? pageSize = 10, string? where = "", string? sort = "")
        {
            string storedProcedureName = StoredProcedures.GetPaging;
            var parameters = new DynamicParameters();
            parameters.Add("@table", string.Format(Tables.View, className));
            parameters.Add("@offset", page);
            parameters.Add("@limit", pageSize);
            parameters.Add("@where", where);
            parameters.Add("@sort", sort);
            PagingResult<P> pagingResult = new PagingResult<P>();
            using (var dbConnection = _storage.GetDbConnection())
            {
                var multipleResults = _storage.QueryMultiple(dbConnection, storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                pagingResult.Data = multipleResults.Read<P>().ToList();
                pagingResult.TotalPage = multipleResults.Read<int>().Single();
                pagingResult.TotalRecord = multipleResults.Read<int>().Single();
            }
            return pagingResult;
        }

        /// <summary>
        /// Lấy thêm dữ liệu phân trang 
        /// </summary>
        /// <param name="page">trang muốn đến</param>
        /// <param name="pageSize">số bản ghi trên 1 trang </param>
        /// <param name="where">câu điều kiện</param>
        /// <param name="sort">câu điều kiện sắp xếp</param>
        /// <returns></returns>
        /// Created by: NNduc(21/03/2023)
        public PagingResult<P> GetPagingResult<P,C>(int? page = 1, int? pageSize = 10, string? where = "", string? sort = "")
        {
            var pagingResult = GetPagingResult<P>(page, pageSize, where, sort);
            string storedProcedureName = string.Format(StoredProcedures.MorePagingInfor, className);
            var parameters = new DynamicParameters();
            parameters.Add("@where", where);
            using (var dbConnection = _storage.GetDbConnection())
            {
                pagingResult.MoreInfo = _storage.QueryFirstOrDefault<C>(dbConnection, storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return pagingResult;
        }
    }
}
