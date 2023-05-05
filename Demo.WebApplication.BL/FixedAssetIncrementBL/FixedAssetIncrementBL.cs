
using Demo.WebApplication.BL.BaseBL;
using Demo.WebApplication.BL.FixedAssetBL;
using Demo.WebApplication.Common;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Entities.DTO.FixedAssetIncrement;
using Demo.WebApplication.Common.Enums;
using Demo.WebApplication.DL.FixedAssetIncrementDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.BL.FixedAssetIncrementBL
{
    public class FixedAssetIncrementBL : BaseBL<FixedAssetIncrement>, IFixedAssetIncrementBL
    {
        IFixedAssetIncrementDL _fixedAssetIncrementDL;
        public FixedAssetIncrementBL(IFixedAssetIncrementDL fixedAssetIncrementDL) : base(fixedAssetIncrementDL)
        {
            _fixedAssetIncrementDL = fixedAssetIncrementDL;
        }

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
        public ServiceResult InsertRecord(FixedAssetIncrementRevision record)
        {
            //Xoá các khoảng cách chuỗi của các property trong bản ghi
            record = (FixedAssetIncrementRevision)TrimStringProperties(record);

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
            if (_fixedAssetIncrementDL.IsCodeExist(code))
            {
                return new ServiceResult
                {
                    ErrorCode = ErrorCode.DuplicateCode,
                    IsSuccess = false,
                    Message = Resource.DevMsg_DulicateCode
                };
            }

            //Thành công -> Gọi vào DL để chạy stored
            var isSuccess = _fixedAssetIncrementDL.InsertRecord(record);

            //Xử lý kết quả trả về
            if (isSuccess)
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
        public ServiceResult UpdateRecord(string id, FixedAssetIncrementRevision record)
        {
            //Xoá các khoảng cách chuỗi của các property trong bản ghi
            id = id.Trim();
            record = (FixedAssetIncrementRevision)TrimStringProperties(record);

            //validate
            var validateResults = ValidateRequestData(record);

            //nếu không có bản ghi nào phù hợp với id hoặc trống, null 
            if (string.IsNullOrEmpty(id) || _fixedAssetIncrementDL.GetRecordsById(id) == null) validateResults.Add(Resource.UserMsg_ValidateId);

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
            if (_fixedAssetIncrementDL.IsCodeExist(code, id))
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
            var isSuccess = _fixedAssetIncrementDL.UpdateRecord(id, record);

            //Xử lý kết quả trả về
            if (isSuccess)
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

        
    }
}
