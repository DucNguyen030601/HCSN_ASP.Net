using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Resources
{
    public struct ValidationAttribute
    {
        public const string RequiredError = "{0} không được để trống.";
        public const string MaxLengthError = "{0} không được quá {1} ký tự.";
        public const string RangeError = "{0} nằm trong khoảng từ {1} đến {2}.";
        public const string RegexGreaterThanZero = "^[1-9][0-9]*$";
        public const string RequiredGreaterThanZero = "{0} phải lớn hơn 0.";
        public struct FixedAsset
        {
            public const string PurchaseProductionDate = "Ngày mua không được lớn hơn ngày sử dụng.";
            public const string DeleteAriseIncrement = "Tài sản có mã {0} đã phát sinh chứng từ ghi tăng có mã {1}.";
            public const string DeleteAriseIncrements = "{0} tài sản được chọn không thể xoá. Vui lòng kiểm tra lại tài sản trước khi thực hiện xoá.";
        }
    }
}
