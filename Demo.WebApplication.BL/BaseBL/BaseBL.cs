using Demo.WebApplication.Common;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Enums;
using Demo.WebApplication.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Filed

        private IBaseDL<T> _baseDL;
        private string className = Functions.ConvertPascalCaseToUnderscore(typeof(T).Name);

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method
        public ServiceResult InsertRecord(T record)
        {
            //validate
            var validateResults = ValidateRequestData(record);

            //Thất bại -> Return lỗi
            if (validateResults.Count > 0)
            {
                return new ServiceResult
                {
                    ErrorCode = ErrorCode.InvalidData,
                    IsSuccess = false,
                    Message = Resource.DevMsg_Validate,
                    Data = validateResults,
                };
            }

            //Kiểm tra có bị trùng mã code không
            var code = (string)record.GetType().GetProperty($"{className}_code").GetValue(record, null);
            if (_baseDL.IsCodeExit(code))
            {
                return new ServiceResult
                {
                    ErrorCode = ErrorCode.DulicateCode,
                    IsSuccess = false,
                    Message = Resource.DevMsg_DulicateCode
                };
            }

            //Thành công -> Gọi vào DL để chạy stored
            var numberAffectedRows = _baseDL.InsertRecord(record);

            //Xử lý kết quả trả về
            if (numberAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = ErrorCode.InsertFailed,
                    Message = Resource.DevMsg_Exception_DL
                };
            }
        }

        public ServiceResult UpdateRecord(string id,T record)
        {
            //validate
            var validateResults = ValidateRequestData(record);

            //nếu không có bản ghi nào phù hợp với id hoặc trống, null 
            if(string.IsNullOrEmpty(id) || _baseDL.GetRecordsById(id)==null) validateResults.Add(Resource.UserMsg_ValidateId);

            //Thất bại -> Return lỗi
            if (validateResults.Count > 0)
            {
                return new ServiceResult
                {
                    ErrorCode = ErrorCode.InvalidData,
                    IsSuccess = false,
                    Message = Resource.DevMsg_Validate,
                    Data = validateResults,
                };
            }

            //Kiểm tra có bị trùng mã code không
            var code = (string)record.GetType().GetProperty($"{className}_code").GetValue(record, null);
            if (_baseDL.IsCodeExit(code,id))
            {
                return new ServiceResult
                {
                    ErrorCode = ErrorCode.DulicateCode,
                    IsSuccess = false,
                    Message = Resource.DevMsg_DulicateCode,
                    Data = validateResults,
                };
            }

            //Thành công -> Gọi vào DL để chạy stored
            var numberAffectedRows = _baseDL.UpdateRecord(id,record);

            //Xử lý kết quả trả về
            if (numberAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = ErrorCode.UpdateFailed,
                    Message = Resource.DevMsg_Exception_DL
                };
            }
        }

        public ServiceResult DeleteRecord(string entityId)
        {
            var validateResults = ""; 
            //nếu không có bản ghi nào phù hợp với id hoặc trống, null 
            if (string.IsNullOrEmpty(entityId) || _baseDL.GetRecordsById(entityId) == null) validateResults = Resource.UserMsg_ValidateId;

            //Thất bại -> Return lỗi
            if (!string.IsNullOrEmpty(validateResults))
            {
                return new ServiceResult
                {
                    ErrorCode = ErrorCode.InvalidData,
                    IsSuccess = false,
                    Message = Resource.DevMsg_Validate,
                    Data = validateResults,
                };
            }

            //Thành công -> Gọi vào DL để chạy stored
            var numberAffectedRows = _baseDL.DeleteRecord(entityId);

            //Xử lý kết quả trả về
            if (numberAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = ErrorCode.DeleteFailed,
                    Message = Resource.DevMsg_Exception_DL
                };
            }

        }

        public List<T> GetRecords()
        {
            return _baseDL.GetRecords();
        }

        public string GetNewCode()
        {
            return _baseDL.GetNewCode();
        }

        public T GetRecordsById(string id)
        {
            return _baseDL.GetRecordsById(id);
        }

        public PagingResult GetPagingResult( int? page = 1, int? pageSize = 10, string? where="",string? sort = "")
        {
            return _baseDL.GetPagingResult( page, pageSize, where, sort);
        }

        /// <summary>
        /// Hà xử lý validate dùng chung cho tất cả class
        /// </summary>
        /// <param name="record">Bản ghi muốn validate</param>
        /// <returns>Danh sách chuỗi bị lỗi</returns>
        private List<string> ValidateRequestData(T record)
        {
            List<string> result = new List<string>();

            //Lấy tất cả các validate của từng property
            var validationResults = new List<ValidationResult>();

            //Muốn lấy bản ghi nào cần validate
            var validationContext = new ValidationContext(record);

            //tất cả các validate trong bản đều đúng -> true, ngược lại
            bool isValid = Validator.TryValidateObject(record, validationContext, validationResults, true);
            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    result.Add(validationResult.ErrorMessage);
                }
            }

            //kiểm tra xem có bị trùng mã không
         /*   string className = Functions.ConvertPascalCaseToUnderscore(typeof(T).Name);
            string code = (string)Functions.GetPropValue(record, $"{className}_code");
            if (_baseDL.IsCodeExit(code) == true)
            {
                result.Add("Mã code không được trùng");
            }*/

            //Lấy thêm validation từ bản custom
            result.AddRange(ValidateCustom(record));
            return result;
        }

        /// <summary>
        /// Hàm xử lý validate không dùng chung nếu muốn validata thay đổi
        /// thì các lớp con kế thừa và thay đổi lại
        /// </summary>
        /// <param name="record">Bản ghi muốn validate</param>
        /// <returns>Danh sách chuỗi bị lỗi</returns>
        protected virtual List<string> ValidateCustom(T record)
        {
            return new List<string>();
        }
        #endregion

    }
}
