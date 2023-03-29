using Dapper;
using Demo.WebApplication.Common;
using Demo.WebApplication.Common.Constants;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Enums;
using Demo.WebApplication.DL.DBConfig;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            var properties = typeof(FixedAsset).GetProperties();
            foreach (var property in properties)
            {
                parameters.Add($"@{property.Name}", property.GetValue(record));
            }

            //Số lượng bản ghi bị ảnh hưởng
            int numberAffectedRows;

            //Kết nối đến ĐB
            using(var dbConnection = _storage.GetDbConnection())
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
        public bool IsCodeExit(string code, string? id=null)
        {
            int numberCodeExit = 0;
          
            //nếu id khác null => trạng thái thêm
            if(string.IsNullOrEmpty(id)) numberCodeExit = NumberCodeExit(code);
            else
            {
                //lấy code cũ (mặc định) ở bản ghi
                string oldCode;

                var storedProcedure = StoredProcedures.GetCode;
                var parameters = new DynamicParameters();
                parameters.Add("@table", className);
                parameters.Add("@id", id);

                //kết nối đến DB
                using (var dbConnection = _storage.GetDbConnection())
                {
                    oldCode = _storage.QueryFirstOrDefault<string>(dbConnection, storedProcedure,parameters, commandType: CommandType.StoredProcedure);
                }

                //nếu mã code cũ (mặc định) khác mã code ban đầu -> kiểm tra trùng mã
                if (oldCode != code)
                {
                    numberCodeExit = NumberCodeExit(code);
                }
            }
            return numberCodeExit == 1;

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
            var properties = typeof(FixedAsset).GetProperties();

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
        public int DeleteRecord(string entityId)
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
                numberAffectedRow = _storage.Execute(dbConnection,storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return numberAffectedRow;
        }

        /// <summary>
        /// Hàm lấy ra mã code mới
        /// </summary>
        /// <returns>Mã code cần lấy</returns>
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
                newCode = _storage.QueryFirstOrDefault<string>(dbConnection, storedProcedureName,paramters, commandType: CommandType.StoredProcedure);
            }
            return newCode;
        }

        /// <summary>
        /// Lấy danh sách tất cả các bản ghi
        /// </summary>
        /// <returns>Danh sách</returns>
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
        private int NumberCodeExit(string code)
        {
            //chuẩn bị stored procedure
            string storedProcedureName = StoredProcedures.DuplicateCode;

            //Chuẩn bị tham số
            var parameters = new DynamicParameters();
            parameters.Add("@table", className);
            parameters.Add("@code", code);

            //Số lượng bản ghi trùng
            int numberCodeExit;

            //kết nối đến DB
            using (var dbConnection = _storage.GetDbConnection())
            {
                numberCodeExit = _storage.QueryFirstOrDefault<int>(dbConnection, storedProcedureName,parameters, commandType: CommandType.StoredProcedure);
            }
            return numberCodeExit;
        }

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id">id bản ghi muốn lấy</param>
        /// <returns>Bản ghi thoả mãn điều kiện</returns>
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
        public virtual PagingResult GetPagingResult(int? page = 1, int? pageSize = 10,string? where ="" ,string? sort = "")
        {
            string storedProcedureName = StoredProcedures.GetPaging;
            var parameters = new DynamicParameters();
            parameters.Add("@table", string.Format(Tables.View,className));
            parameters.Add("@offset", page);
            parameters.Add("@limit", pageSize);
            parameters.Add("@where", where);
            parameters.Add("@sort", sort);
            PagingResult pagingResult = new PagingResult();
            using (var dbConnection = _storage.GetDbConnection())
            {
                var multipleResults = _storage.QueryMultiple(dbConnection, storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                pagingResult.Data = multipleResults.Read().ToList();
                pagingResult.TotalPage = multipleResults.Read<int>().Single();
                pagingResult.TotalRecord = multipleResults.Read<int>().Single();
            }
            return pagingResult;
        }
    }
}
