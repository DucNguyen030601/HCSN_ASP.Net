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
                else if (!result.IsSuccess && result.ErrorCode == ErrorCode.DulicateCode)
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

        [HttpPut("{id}")]
        public IActionResult UpdateRecord(string id, T record)
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
                else if (!result.IsSuccess && result.ErrorCode == ErrorCode.DulicateCode)
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
