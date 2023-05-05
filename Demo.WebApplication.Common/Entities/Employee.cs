using Demo.WebApplication.Common.BaseVA;
using Demo.WebApplication.Common.Enums;
using Demo.WebApplication.Common.Resources;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using ValidationAttribute = Demo.WebApplication.Common.Resources.ValidationAttribute;

namespace Demo.WebApplication.Common.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// khoá chính
        /// </summary>

        [Key]
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Mã tài sản không được để trống")]
        [RegularExpression(@"^[A-Z]{2}[0-9]{5}$",
         ErrorMessage = "Không đúng định dạng mã.")]
        public string? EmployeeCode { get; set; }

        [BaseRequired]
        //[BaseRegex(ValidationAttribute.FixedAsset.RegexPattern,
        //           ValidationAttribute.FixedAsset.RegexExample)]
        public string? EmployeeName { get; set; }

        [EnumDataType(typeof(Gender), ErrorMessage = "aaa")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [BaseRegex(ValidationAttribute.RegexGreaterThanZero,ValidationAttribute.RequiredGreaterThanZero)]
        public int? SoLuong { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreateBy { get; set; }

    }
}
