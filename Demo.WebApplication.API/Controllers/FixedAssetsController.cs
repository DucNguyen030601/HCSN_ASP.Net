
using Demo.WebApplication.API.lib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using Dapper;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common;
using Demo.WebApplication.Common.Enums;
using Demo.WebApplication.DL.FixedAssetDL;
using Demo.WebApplication.BL.FixedAssetBL;
using MySql.Data.MySqlClient;
using Demo.WebApplication.BL.BaseBL;

namespace Demo.WebApplication.API.Controllers
{
    public class FixedAssetsController :BaseController<FixedAsset>
    {
        IFixedAssetBL _fixedAssetBL;
        public FixedAssetsController(IFixedAssetBL fixedAssetBL) : base(fixedAssetBL)
        {
            _fixedAssetBL = fixedAssetBL;
        }
        [HttpGet("Filter")]
        public IActionResult GetPagingResult(int? page = 1, int? pageSize = 10,string? sort = "", string departmentName = "", string fixedAssetCategoryName = "", string filter = "" )
        {
            try
            {
                string where = string.Format("" +
                           "department_name like '%{0}%' " +
                           "AND fixed_asset_category_name like '%{1}%' " +
                           "AND ( fixed_asset_name like '%{2}%' " +
                           "OR fixed_asset_code like '%{2}%')",
                           departmentName, fixedAssetCategoryName, filter);
                return StatusCode(StatusCodes.Status200OK, _fixedAssetBL.GetPagingResult(page, pageSize, where, sort));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = ex.Message,
                    TraceId = HttpContext.TraceIdentifier
                });
            }

        }
        
        [HttpGet("NewFixedAssetCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _fixedAssetBL.GetNewCode());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = ex.Message,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }
    }
}
