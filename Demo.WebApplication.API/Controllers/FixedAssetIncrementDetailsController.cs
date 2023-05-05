using Demo.WebApplication.BL.BaseBL;
using Demo.WebApplication.Common;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO.FixedAsset;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Demo.WebApplication.BL.FixedAssetIncrementBL;
using Demo.WebApplication.BL.FixedAssetIncrementDetailBL;
using Demo.WebApplication.Common.Entities.DTO.FixedAssetIncrementDetail;

namespace Demo.WebApplication.API.Controllers
{
    public class FixedAssetIncrementDetailsController : BaseController<FixedAssetIncrementDetail>
    {
        IFixedAssetIncrementDetailBL _fixedAssetIncrementDetailBL;
        public FixedAssetIncrementDetailsController(IFixedAssetIncrementDetailBL fixedAssetIncrementDetailBL) : base(fixedAssetIncrementDetailBL)
        {
            _fixedAssetIncrementDetailBL = fixedAssetIncrementDetailBL;
        }
        [HttpPost("Filter")]
        public IActionResult GetPagingResult(string entityId, int? page = 0, int? pageSize = -1, string? sort = "", string filter = "")
        {
            try
            {
                string where;
                if (entityId == null)
                {
                    where = string.Format("" +
                           "fixed_asset_name like '%{0}%' " +
                           "OR fixed_asset_code like '%{0}%'",
                           filter);
                }
                else
                {
                    where = string.Format("" +
                                               "fixed_asset_increment_id = '{0}' " +
                                               "AND ( fixed_asset_name like '%{1}%' " +
                                               "OR fixed_asset_code like '%{1}%')",
                                               entityId, filter);
                }
                return StatusCode(StatusCodes.Status200OK, _fixedAssetIncrementDetailBL.GetPagingResult<FixedAssetResult>(page, pageSize, where, sort));
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
    }
}
