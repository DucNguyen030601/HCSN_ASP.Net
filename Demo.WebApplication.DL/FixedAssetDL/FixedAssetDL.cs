using Dapper;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.DL.DBConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Demo.WebApplication.DL.FixedAssetDL
{
    public class FixedAssetDL : IFixedAssetDL
    {

        #region Field

        private IStorage _storage;

        #endregion

        #region Contructor

        public FixedAssetDL(IStorage storage)
        {
            _storage = storage;
        }

        #endregion
        /// <summary>
        /// Hàm thêm 1 mới 1 bản ghi Tài sản
        /// </summary>
        /// <param name="fixedAsset">Đối tượng muốn thêm</param>
        /// <returns>
        /// 1: thêm thành công
        /// 0: thêm thất bại
        /// </returns>
        public int InsertFixedAsset(FixedAsset fixedAsset)
        {
            //Chuẩn bị tên stored procedure
            string storedProcedure = "PROC_fixed_asset_Insert";

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();
            parameters.Add("");

            //Số lượng bản ghi bị ảnh hưởng
            int numberAffectedRows;
            
            //Kết nối đến ĐB
            var dbConnection = _storage.GetDbConnection();

            //Gọi vào ĐB
            numberAffectedRows = _storage.Execute(dbConnection,storedProcedure, parameters,commandType:CommandType.StoredProcedure);
            return numberAffectedRows;
        }

        public List<FixedAsset> GetFixedAssetList()
        {

            //Chuẩn bị tên View
            string viewName = "view_fixed_asset";

            //Danh sách bản ghi
            List<FixedAsset> fixedAssets;

            //Kết nối đến ĐB
            var dbConnection = _storage.GetDbConnection();

            fixedAssets = dbConnection.Query<FixedAsset>(viewName, commandType: CommandType.TableDirect).ToList();

            return fixedAssets;
        }



    }
}
