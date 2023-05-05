using Demo.WebApplication.BL.BaseBL;
using Demo.WebApplication.BL.FixedAssetIncrementBL;
using Demo.WebApplication.Common;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO.FixedAssetIncrement;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Demo.WebApplication.Common.Entities.DTO.FixedAsset;
using Demo.WebApplication.DL.FixedAssetIncrementDL;

namespace Demo.WebApplication.API.Controllers
{
    public class FixedAssetIncrementsController : BaseController<FixedAssetIncrement>
    {
        IFixedAssetIncrementBL _fixedAssetIncrementBL;
        public FixedAssetIncrementsController(IFixedAssetIncrementBL fixedAssetIncrementBL) : base(fixedAssetIncrementBL)
        {
            _fixedAssetIncrementBL = fixedAssetIncrementBL;
        }

        [HttpGet("Filter")]
        public IActionResult GetPagingResult(int? page = 1, int? pageSize = 10, string? sort = "", string filter = "")
        {
            try
            {
                string where = string.Format("" +
                           " ( description like '%{0}%' " +
                           "OR fixed_asset_increment_code like '%{0}%')", filter);
                return StatusCode(StatusCodes.Status200OK, _fixedAssetIncrementBL.GetPagingResult<FixedAssetIncrementResult,FixedAssetIncrementMoreInfor>(page, pageSize, where, sort));
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

        [HttpGet("NewFixedAssetIncrementCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                var newCode = _fixedAssetIncrementBL.GetNewCode();
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
        /// Hàm thêm mới bản ghi và thêm các bản ghi chi tiết
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// StatusCode -> 201: Thêm thành công, 400: Lỗi phía người dùng (Đầu vào không hợp lệ, validate,..)
        /// StatusData -> ErrorResult:
        /// ErrorCode: Lưu mã code bên phía BL trả về
        /// DevMsg: Lưu thông tin khi lỗi bên phía BL trả về
        /// MoreInfo: Lưu thêm các thông tin khi lỗi bên phía BL trả về
        /// TraceId: Lưu id trong quá trình thực hiện thêm mới bản ghi
        /// </returns>
        /// AUTHOR NNduc (03/04/2023)
        [HttpPost("InsertMultiple")]
        public IActionResult InsertRecord([FromBody] FixedAssetIncrementRevision record)
        {
            try
            {
                var result = _fixedAssetIncrementBL.InsertRecord(record);

                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (!result.IsSuccess && result.ErrorCode == ErrorCode.InvalidData)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        UserMsg = Resource.UserMsg_Validate,
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
                else if (!result.IsSuccess && result.ErrorCode == ErrorCode.DuplicateCode)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        UserMsg = Resource.UserMsg_Validate,
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        UserMsg = Resource.UserMsg_Exception,
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
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
        /// Hàm sửa bản ghi và thêm,sửa các bản ghi chi tiết
        /// </summary>
        /// <param name="id">ID bản ghi muốn sửa</param>
        /// <param name="record">Bản ghi muốn sửa</param>
        /// <returns>
        /// StatusCode -> 200: Sửa thành công, 400: Lỗi phía người dùng (Đầu vào không hợp lệ, validate,..)
        /// StatusData -> ErrorResult:
        /// ErrorCode: Lưu mã code bên phía BL trả về
        /// DevMsg: Lưu thông tin khi lỗi bên phía BL trả về
        /// MoreInfo: Lưu thêm các thông tin khi lỗi bên phía BL trả về
        /// TraceId: Lưu id trong quá trình thực hiện thêm mới bản ghi
        /// </returns>
        /// AUTHOR NNduc (03/04/2023)
        [HttpPut("UpdateMultiple/{id}")]
        public IActionResult UpdateRecord(string id, [FromBody] FixedAssetIncrementRevision record)
        {
            try
            {
                var result = _fixedAssetIncrementBL.UpdateRecord(id, record);

                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else if (!result.IsSuccess && result.ErrorCode == ErrorCode.InvalidData)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        UserMsg = Resource.UserMsg_Validate,
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
                else if (!result.IsSuccess && result.ErrorCode == ErrorCode.DuplicateCode)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        UserMsg = Resource.UserMsg_Validate,
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        UserMsg = Resource.UserMsg_Exception,
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
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
