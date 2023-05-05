using Demo.WebApplication.Common;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Enums;
using Demo.WebApplication.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Filed

        private IBaseDL<T> _baseDL;

        /// <summary>
        /// Lưu tên class dưới dạng underscore (VD: ClassA -> class_b)
        /// </summary>
        protected string className = Functions.ConvertPascalCaseToUnderscore(typeof(T).Name);

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method
        /// <summary>
        /// Hàm thêm mới bản ghi
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// Trả về 1 đối tượng Service Result
        /// ISuccess -> true thành công, ngược lại
        /// ErrorCode -> mã code nếu không thành công
        /// Data -> Miêu tả rõ hơn về lỗi (validate, trùng mã,...)
        /// Message -> Thêm thông tin cho phía Dev
        /// </returns>
        public ServiceResult InsertRecord(T record)
        {
            //Xoá các khoảng cách chuỗi của các property trong bản ghi
            record = TrimStringProperties(record);

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
            if (_baseDL.IsCodeExist(code))
            {
                return new ServiceResult
                {
                    ErrorCode = ErrorCode.DuplicateCode,
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

        /// <summary>
        /// Hàm sửa bản ghi
        /// </summary>
        /// <param name="record">Bản ghi muốn sửa</param>
        /// <param name="id">Id bản ghi muốn sửa</param>
        /// <returns>
        /// Trả về 1 đối tượng Service Result
        /// ISuccess -> true thành công, ngược lại
        /// ErrorCode -> mã code nếu không thành công
        /// Data -> Miêu tả rõ hơn về lỗi (validate, trùng mã,...)
        /// Message -> Thêm thông tin cho phía Dev
        /// </returns>
        public ServiceResult UpdateRecord(string id, T record)
        {
            //Xoá các khoảng cách chuỗi của các property trong bản ghi
            id = id.Trim();
            record = TrimStringProperties(record);

            //validate
            var validateResults = ValidateRequestData(record);

            //nếu không có bản ghi nào phù hợp với id hoặc trống, null 
            if (string.IsNullOrEmpty(id) || _baseDL.GetRecordsById(id) == null) validateResults.Add(Resource.UserMsg_ValidateId);

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
            var code = (string)record.GetType().GetProperty($"{className}_code").GetValue(record);
            if (_baseDL.IsCodeExist(code, id))
            {
                return new ServiceResult
                {
                    ErrorCode = ErrorCode.DuplicateCode,
                    IsSuccess = false,
                    Message = Resource.DevMsg_DulicateCode,
                    Data = validateResults,
                };
            }

            //Thành công -> Gọi vào DL để chạy stored
            var numberAffectedRows = _baseDL.UpdateRecord(id, record);

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

        /// <summary>
        /// Hàm xoá bản ghi
        /// </summary>
        /// <param name="entityId">Id bản ghi muốn thêm</param>
        /// <returns>
        /// Trả về 1 đối tượng Service Result
        /// ISuccess -> true thành công, ngược lại
        /// ErrorCode -> mã code nếu không thành công
        /// Data -> Miêu tả rõ hơn về lỗi (validate, trùng mã,...)
        /// Message -> Thêm thông tin cho phía Dev
        /// </returns>
        public ServiceResult DeleteRecord(string entityId)
        {
            entityId = entityId.Trim();
            var validateResults = ValidateDeleteRecord(entityId);
            //nếu không có bản ghi nào phù hợp với id hoặc trống, null 
            //if (string.IsNullOrEmpty(entityId) || _baseDL.GetRecordsById(entityId) == null) validateResults = Resource.UserMsg_ValidateId;

            //Thất bại -> Return lỗi
            if (validateResults.Count() > 0)
            {
                return new ServiceResult
                {
                    ErrorCode = ErrorCode.DeleteArised,
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

        /// <summary>
        /// Hàm xoá nhiều bản ghi
        /// </summary>
        /// <param name="entityIds">Bản ghi muốn thêm</param>
        /// <returns>
        /// Trả về 1 đối tượng Service Result
        /// ISuccess -> true thành công, ngược lại
        /// ErrorCode -> mã code nếu không thành công
        /// Data -> Miêu tả rõ hơn về lỗi (validate, trùng mã,...)
        /// Message -> Thêm thông tin cho phía Dev
        /// </returns>
        public ServiceResult DeleteRecords(List<string> entityIds)
        {
            entityIds = TrimStringElements(entityIds);
            var validateResults = ValidateDeleteRecords(entityIds);

            //Thất bại -> Return lỗi
            if (validateResults.Count() > 0)
            {
                return new ServiceResult
                {
                    ErrorCode = ErrorCode.DeleteArised,
                    IsSuccess = false,
                    Message = Resource.DevMsg_Validate,
                    Data = validateResults,
                };
            }

            //Thành công -> Gọi vào DL để chạy stored
            var isSuccess = _baseDL.DeleteRecords(entityIds);

            //Xử lý kết quả trả về
            if (isSuccess)
            {
                return new ServiceResult
                {
                    IsSuccess = isSuccess
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = isSuccess,
                    ErrorCode = ErrorCode.DeleteFailed,
                    Message = Resource.DevMsg_Exception_DL
                };
            }
        }

        /// <summary>
        /// Hàm lấy tất cả bản ghi mặc định
        /// </summary>
        /// <returns>Danh sách các bản ghi</returns>
        public List<T> GetRecords()
        {
            return _baseDL.GetRecords();
        }

        /// <summary>
        /// Lấy mã code code
        /// </summary>
        /// <returns>
        /// string: lấy được mã code
        /// null: không lấy được mã code
        /// </returns>
        public string GetNewCode()
        {
            return _baseDL.GetNewCode();
        }

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="id">id bản ghi muốn lấy</param>
        /// <returns>
        /// T: Lấy được bản ghi
        /// null: Không lấy được bản ghi
        /// </returns>
        public T GetRecordsById(string id)
        {
            return _baseDL.GetRecordsById(id);
        }

        /// <summary>
        /// Hàm lấy tất cả bản ghi có phân trang
        /// </summary>
        /// <param name="page">trang muốn đến</param>
        /// <param name="pageSize">số lượng bản ghi trong 1 trang</param>
        /// <param name="where">Câu điều kiện</param>
        /// <param name="sort">Câu điều kiện sắp xếp</param>
        /// <returns>
        /// Đối tượng bao gồm thuộc tính:
        /// Data -> Danh sách bản ghi trên 1 trang
        /// TotalRecord -> tổng số bản ghi
        /// TotalPage -> tổng số trang
        /// MoreInfor -> Dữ liệu muốn lấy thêm (Tổng số tiền,...)
        /// </returns>
        public PagingResult<P> GetPagingResult<P>(int? page = 1, int? pageSize = 10, string? where = "", string? sort = "")
        {
            return _baseDL.GetPagingResult<P>(page, pageSize, where, sort);
        }

        /// <summary>
        /// Hà xử lý validate dùng chung cho tất cả class
        /// </summary>
        /// <param name="record">Bản ghi muốn validate</param>
        /// <returns>Danh sách chuỗi bị lỗi</returns>
        protected List<string> ValidateRequestData(T record)
        {
            List<string> result = new List<string>();

            if (record == null)
            {
                result.Add(Resource.UserMsg_Validate);
                return result;
            }

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
            if (result.Count == 0) result.AddRange(ValidateCustom(record));

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

        /// <summary>
        /// Hàm xử lý validate cho xoá 1 bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Mảng danh sách các trường lỗi</returns>
        /// Author: NNduc (4/5/2023)
        protected virtual List<string> ValidateDeleteRecord(string entityId)
        {
            return new List<string>();
        }

        /// <summary>
        /// Hàm xử lý validate cho xoá nhiều bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Mảng danh sách các trường lỗi</returns>
        /// Author: NNduc (4/5/2023)
        protected virtual List<string> ValidateDeleteRecords(List<string> entityIds)
        {
            return new List<string>();
        }

        /// <summary>
        /// Hàm để xoá các khoảng cách cách chuỗi
        /// </summary>
        /// <param name="record">bản ghi muốn thực hiện</param>
        /// <returns>bản ghi đã thực hiện</returns>
        /// Author NNduc(04/11/2023)
        public T TrimStringProperties(T record)
        {
            var stringProperties = record.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));

            foreach (PropertyInfo propertyInfo in stringProperties)
            {
                string value = (string)propertyInfo.GetValue(record);
                if (value != null)
                    propertyInfo.SetValue(record, value.Trim());
            }
            return record;
        }

        /// <summary>
        /// Hàm xoá tất cả các các khoảng trống của chuỗi List
        /// </summary>
        /// <param name="strings">Danh sách muốn thực hiện</param>
        /// <returns>Bản ghi đã thực hiện</returns>
        /// Author NNduc(04/11/2023)
        public List<string> TrimStringElements(List<string> strings)
        {
            return strings.Select(s=>s.Trim()).ToList();
        }

        /// <summary>
        /// Lấy thêm dữ liệu phân trang 
        /// </summary>
        /// <param name="page">trang muốn đến</param>
        /// <param name="pageSize">số bản ghi trên 1 trang </param>
        /// <param name="where">câu điều kiện</param>
        /// <param name="sort">câu điều kiện sắp xếp</param>
        /// <returns></returns>
        public PagingResult<P> GetPagingResult<P, C>(int? page = 1, int? pageSize = 10, string? where = "", string? sort = "")
        {
            return _baseDL.GetPagingResult<P, C>(page, pageSize, where, sort);
        }

        #endregion
    }
}
