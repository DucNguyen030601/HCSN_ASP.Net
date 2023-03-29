using Dapper;
using Demo.WebApplication.Common.Constants;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.DL.BaseDL;
using Demo.WebApplication.DL.DBConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Demo.WebApplication.DL.FixedAssetDL
{
    public class FixedAssetDL : BaseDL<FixedAsset>, IFixedAssetDL
    {
        public FixedAssetDL(IStorage storage) : base(storage)
        {
        }

        public override PagingResult GetPagingResult(int? page = 1, int? pageSize = 10, string? where = "", string? sort = "")
        {
            var pagingResult = base.GetPagingResult(page, pageSize, where, sort);
            string storedProcedureName = string.Format(StoredProcedures.MorePagingInfor,className);
            var parameters = new DynamicParameters();
            parameters.Add("@where", where);
            using (var dbConnection = _storage.GetDbConnection())
            {
                pagingResult.MoreInfo  = _storage.QueryFirstOrDefault<object>(dbConnection, storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return pagingResult;
        }
    }
}
