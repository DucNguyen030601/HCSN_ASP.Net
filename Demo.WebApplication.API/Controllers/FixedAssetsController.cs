
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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Globalization;
using Demo.WebApplication.Common.Entities.DTO.FixedAsset;
using Demo.WebApplication.Common.Entities.DTO.FixedAssetIncrementDetail;

namespace Demo.WebApplication.API.Controllers
{
    public class FixedAssetsController : BaseController<FixedAsset>
    {
        private IFixedAssetBL _fixedAssetBL;
        public FixedAssetsController(IFixedAssetBL fixedAssetBL) : base(fixedAssetBL)
        {
            _fixedAssetBL = fixedAssetBL;
        }
        /// <summary>
        /// Lấy danh sách tài sản theo bộ lọc
        /// </summary>
        /// <param name="page">trang muốn lấy</param>
        /// <param name="pageSize">số lượng bản ghi trên 1 trang</param>
        /// <param name="sort">sắp xếp</param>
        /// <param name="departmentName">tìm kiếm theo tên phòng ban</param>
        /// <param name="fixedAssetCategoryName">tìm kiếm theo tên bộ phận sử dụng</param>
        /// <param name="filter">tìm kiếm theo mã tài sản or tên tài sản</param>
        /// <returns>danh sách bản ghi</returns>
        /// Author: NNDuc (30/4/2023)
        [HttpGet("Filter")]
        public IActionResult GetPagingResult(int? page = 1, int? pageSize = 10, string? sort = "", string departmentName = "", string fixedAssetCategoryName = "", string filter = "")
        {
            try
            {
                string where = string.Format("" +
                           "department_name like '%{0}%' " +
                           "AND fixed_asset_category_name like '%{1}%' " +
                           "AND ( fixed_asset_name like '%{2}%' " +
                           "OR fixed_asset_code like '%{2}%')",
                           departmentName, fixedAssetCategoryName, filter);
                return StatusCode(StatusCodes.Status200OK, _fixedAssetBL.GetPagingResult<FixedAssetResult, FixedAssetMoreInfor>(page, pageSize, where, sort));
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

        /// <summary>
        /// Lấy danh sách tài sản theo bộ lọc
        /// </summary>
        /// <param name="page">trang muốn lấy</param>
        /// <param name="pageSize">số lượng bản ghi trên 1 trang</param>
        /// <param name="sort">sắp xếp</param>
        /// <param name="departmentName">tìm kiếm theo tên phòng ban</param>
        /// <param name="fixedAssetCategoryName">tìm kiếm theo tên bộ phận sử dụng</param>
        /// <param name="filter">tìm kiếm theo mã tài sản or tên tài sản</param>
        /// <returns>danh sách bản ghi</returns>
        /// Author: NNDuc (30/4/2023)
        [HttpPost("IncrementFilter")]
        public IActionResult GetPagingResult([FromBody] FixedAssetIdFilter fixedAssetIdFilter,int? page = 1, int? pageSize = 10, string? sort = "", string filter = "")
        {
            try
            {
                string stringInEntityIds = string.Join("','", fixedAssetIdFilter.inEntityIds);
                string stringNotInEntityIds = string.Join("','", fixedAssetIdFilter.notInEntityIds);
                string where = string.Format("" +
                           "((fixed_asset_increment_code IS NULL " +
                           "AND fixed_asset_id NOT IN ('{0}')) " +
                           "OR fixed_asset_id IN ('{1}')) " +
                           "AND ( fixed_asset_name like '%{2}%' " +
                           "OR fixed_asset_code like '%{2}%')",
                           stringNotInEntityIds, stringInEntityIds, filter);
                return StatusCode(StatusCodes.Status200OK, _fixedAssetBL.GetPagingResult<FixedAssetResult, FixedAssetIncrementDetailMoreInfor>(page, pageSize, where, sort));
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

        /// <summary>
        /// Lấy mã code mới
        /// </summary>
        /// <returns>Mã code</returns>
        /// Author NNduc (15/4/2023)
        [HttpGet("NewFixedAssetCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                var newCode = _fixedAssetBL.GetNewCode();
                if (string.IsNullOrEmpty(newCode))
                {
                    return NotFound();
                }
                else return StatusCode(StatusCodes.Status200OK, newCode);
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

        /// <summary>
        /// Xuất file Excel
        /// </summary>
        /// <param name="page">trang muốn lấy</param>
        /// <param name="pageSize">số lượng bản ghi trên 1 trang</param>
        /// <param name="sort">sắp xếp</param>
        /// <param name="departmentName">tìm kiếm theo tên phòng ban</param>
        /// <param name="fixedAssetCategoryName">tìm kiếm theo tên bộ phận sử dụng</param>
        /// <param name="filter">tìm kiếm theo mã tài sản or tên tài sản</param>
        /// <returns>danh sách bản ghi</returns>
        /// Author: NNDuc (30/4/2023)
        [HttpPost("ExportExcel")]
        public IActionResult ExportExcel(string? sort = "", string departmentName = "", string fixedAssetCategoryName = "", string filter = "")
        {
            try
            {
                string where = string.Format("" +
                               "department_name like '%{0}%' " +
                               "AND fixed_asset_category_name like '%{1}%' " +
                               "AND ( fixed_asset_name like '%{2}%' " +
                               "OR fixed_asset_code like '%{2}%')",
                               departmentName, fixedAssetCategoryName, filter);
                var stream = _fixedAssetBL.ExportExcel(sort, where);
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "Danh_Sach_Tai_San.xlsx";
                return File(stream, contentType, fileName);
            }
            catch (Exception ex)
            {
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
