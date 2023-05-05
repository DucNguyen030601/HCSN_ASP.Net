using Demo.WebApplication.BL.BaseBL;
using Demo.WebApplication.Common;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Session;
using MySqlX.XDevAPI.Common;

namespace Demo.WebApplication.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {

        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constuctor

        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        /// <summary>
        /// Hàm thêm mới bản ghi
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
        [HttpPost]
        public IActionResult InsertRecord([FromBody]T record)
        {
            try
            {
                var result = _baseBL.InsertRecord(record);

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
        /// Hàm sửa bản ghi
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
        [HttpPut("{id}")]
        public IActionResult UpdateRecord(string id, [FromBody]T record)
        {
            try
            {
                var result = _baseBL.UpdateRecord(id, record);

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

        /// <summary>
        /// Hàm xoá bản ghi
        /// </summary>
        /// <param name="entityID">Id Bản ghi muốn xoá</param>
        /// <returns>
        /// StatusCode -> 200: Xoá thành công, 400: Lỗi phía người dùng (Đầu vào không hợp lệ, validate,..)
        /// StatusData -> ErrorResult:
        /// ErrorCode: Lưu mã code bên phía BL trả về
        /// DevMsg: Lưu thông tin khi lỗi bên phía BL trả về
        /// MoreInfo: Lưu thêm các thông tin khi lỗi bên phía BL trả về
        /// TraceId: Lưu id trong quá trình thực hiện thêm mới bản ghi
        /// </returns>
        /// AUTHOR NNduc (03/04/2023)
        [HttpDelete("{entityID}")]
        public IActionResult DeleteRecord(string entityID)
        {
            try
            {
                var result = _baseBL.DeleteRecord(entityID);

                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                //else if (!result.IsSuccess && result.ErrorCode == ErrorCode.InvalidData)
                //{
                //    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                //    {
                //        ErrorCode = result.ErrorCode,
                //        DevMsg = result.Message,
                //        UserMsg = Resource.UserMsg_Validate,
                //        MoreInfo = result.Data,
                //        TraceId = HttpContext.TraceIdentifier
                //    });
                //}
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
        /// Hàm xoá nhiều bản ghi
        /// </summary>
        /// <param name="entityIDs">Danh sách id muốn xoá</param>
        /// <returns>
        /// StatusCode -> 200: Xoá thành công, 400: Lỗi phía người dùng (Đầu vào không hợp lệ, validate,..)
        /// StatusData -> ErrorResult:
        /// ErrorCode: Lưu mã code bên phía BL trả về
        /// DevMsg: Lưu thông tin khi lỗi bên phía BL trả về
        /// MoreInfo: Lưu thêm các thông tin khi lỗi bên phía BL trả về
        /// TraceId: Lưu id trong quá trình thực hiện thêm mới bản ghi
        /// </returns>
        /// AUTHOR NNduc (03/04/2023)
        [HttpDelete]
        public IActionResult DeleteRecords([FromBody] List<string> entityIDs)
        {
            try
            {
                var result = _baseBL.DeleteRecords(entityIDs);

                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status200OK);
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
        /// Hàm lấy bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi muốn lấy</param>
        /// <returns>
        /// StatusCode -> 200: lấy thành công, 404: Lỗi không tìm thấy
        /// StatusData -> ErrorResult:
        /// ErrorCode: Lưu mã code bên phía BL trả về
        /// DevMsg: Lưu thông tin khi lỗi bên phía BL trả về
        /// MoreInfo: Lưu thêm các thông tin khi lỗi bên phía BL trả về
        /// TraceId: Lưu id trong quá trình thực hiện thêm mới bản ghi
        /// </returns>
        /// AUTHOR NNduc (03/04/2023)
        [HttpGet("{id}")]
        public IActionResult GetRecordsById(string id)
        {
            try
            {
                var record = _baseBL.GetRecordsById(id);
                if (record == null)
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, record);
                }
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

        /// <summary>
        /// Hàm lấy danh sách bản ghi
        /// </summary>
        /// <returns>
        /// StatusCode -> 200: Lấy thành công, 400: Không tìm thấy bản ghi
        /// StatusData -> ErrorResult:
        /// ErrorCode: Lưu mã code bên phía BL trả về
        /// DevMsg: Lưu thông tin khi lỗi bên phía BL trả về
        /// MoreInfo: Lưu thêm các thông tin khi lỗi bên phía BL trả về
        /// TraceId: Lưu id trong quá trình thực hiện thêm mới bản ghi
        /// </returns>
        /// AUTHOR NNduc (03/04/2023)
        [HttpGet]
        public IActionResult GetRecords()
        {
            try
            {
                var records = _baseBL.GetRecords();
                if (records.Count() == 0)
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }
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
