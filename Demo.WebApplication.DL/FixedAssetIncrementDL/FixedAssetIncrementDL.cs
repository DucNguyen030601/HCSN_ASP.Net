using Dapper;
using Demo.WebApplication.Common.Constants;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO.FixedAsset;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.DL.BaseDL;
using Demo.WebApplication.DL.DBConfig;
using Demo.WebApplication.DL.FixedAssetDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.WebApplication.Common.Entities.DTO.FixedAssetIncrement;

namespace Demo.WebApplication.DL.FixedAssetIncrementDL
{
    public class FixedAssetIncrementDL : BaseDL<FixedAssetIncrement>, IFixedAssetIncrementDL
    {

        public FixedAssetIncrementDL(IStorage storage) : base(storage)
        {
        }

        /// <summary>
        /// Hàm thêm mới 1 bản ghi chi tiết
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created by: NNduc(21/03/2023)
        public bool InsertRecord(FixedAssetIncrementRevision record)
        {
            //Các thành phần ảnh hưởng đến câu lệnh
            int elementInsert = 1;
            int elementUpdate = record.fixed_asset_id.Count;

            //Chuẩn bị tham số đầu vào cho stored
            string storedProcedureName = string.Format(StoredProcedures.InsertMultiple, className);

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            var properties = typeof(FixedAssetIncrement).GetProperties();

            foreach (var property in properties)
            {
                parameters.Add($"@{property.Name}", property.GetValue(record));
            }

            string entityIdString = "'" + string.Join("','", record.fixed_asset_id) + "'";

            parameters.Add("@fixed_asset_id", entityIdString);

            //Số lượng bản ghi bị ảnh hưởng
            int numberAffectedRows;

            //Số lượng thành phần bị ảnh hưởng
            //mặc định + thành phần update + thành phần insert
            int numberElementAffectedRows = elementInsert + elementUpdate * 2;

            using (var dbConnection = _storage.GetDbConnection())
            {

                using (var dbTrancation = _storage.GetDbTransaction(dbConnection))
                {
                    try
                    {
                        var multipleResults = _storage.QueryMultiple(dbConnection, storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                        numberAffectedRows = multipleResults.Read<int>().Single();
                        if (numberElementAffectedRows != numberAffectedRows) { _storage.Rollback(dbTrancation); return false; }
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
            return true;
        }

        /// <summary>
        /// Hàm sửa 1 bản ghi chi tiết
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created by: NNduc(21/03/2023)
        public bool UpdateRecord(string id, FixedAssetIncrementRevision record)
        {
            //Các thành phần ảnh hưởng đến câu lệnh
            int elementInsert = 1;
            int elementUpdate = record.fixed_asset_id.Count;

            //Chuẩn bị tham số đầu vào cho stored
            string storedProcedureName = string.Format(StoredProcedures.UpdateMultiple, className);

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            var properties = typeof(FixedAssetIncrement).GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == $"{className}_id") continue; //không thêm parameter nếu property là id
                parameters.Add($"@{property.Name}", property.GetValue(record));
            }

            //Thêm parameter id
            parameters.Add($"@{className}_id", id);

            string entityIdString = "'" + string.Join("','", record.fixed_asset_id) + "'";
            parameters.Add("@fixed_asset_id", entityIdString);

            //Số lượng bản ghi bị ảnh hưởng
            int numberAffectedRows;

            //Số lượng thành phần bị ảnh hưởng
            //mặc định + thành phần update + thành phần insert
            int numberElementAffectedRows = elementInsert + elementUpdate * 2;

            using (var dbConnection = _storage.GetDbConnection())
            {
                using (var dbTrancation = _storage.GetDbTransaction(dbConnection))
                {
                    try
                    {
                        var multipleResults = _storage.QueryMultiple(dbConnection, storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                        numberAffectedRows = multipleResults.Read<int>().Single();
                        if (numberElementAffectedRows != numberAffectedRows) { _storage.Rollback(dbTrancation); return false; }
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
        /// Hàm xoá 1 bản ghi
        /// </summary>
        /// <param name="entityId">Mã id của thực thể cần xoá</param>
        /// <returns>
        /// Số bản ghi bị ảnh hưởng
        /// </returns>
        public override int DeleteRecord(string entityId)
        {
            //Chuẩn bị tham số đầu vào cho stored
            string storedProcedureName = string.Format(StoredProcedures.Delete, className);

            //truyền tham số vào
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{className}_id", entityId);

            //Số lượng bản ghi bị ảnh hưởng
            var numberAffectedRow = 0;
            using (var dbConnection = _storage.GetDbConnection())
            {
                using (var dbTrancation = _storage.GetDbTransaction(dbConnection))
                {
                    try
                    {
                        numberAffectedRow = _storage.QueryMultiple(dbConnection, storedProcedureName, parameters, dbTrancation, commandType: CommandType.StoredProcedure).Read<int>().Single();
                        if(numberAffectedRow < 1) { _storage.Rollback(dbTrancation); return 0; } 
                        _storage.Commit(dbTrancation);
                        return numberAffectedRow;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _storage.Rollback(dbTrancation);
                        return numberAffectedRow;
                    }
                }
            }
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
        public override bool DeleteRecords(List<string> entityIds)
        {
            string entityIdString = "'" + string.Join("','", entityIds) + "'";
            int countRecord = entityIds.Count;
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
                        numberAffectedRow = _storage.QueryMultiple(dbConnection, storedProcedureName, parameters, commandType: CommandType.StoredProcedure).Read<int>().Single();
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
    }
}
