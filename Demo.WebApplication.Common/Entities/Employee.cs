using Demo.WebApplication.Common.Enums;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage ="Mã nhân viên không được để trống")]
        [MaxLength(10,ErrorMessage ="Mã nhân viên phải ít nhất 10")]
        public string EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreateBy { get; set; }

    }
}
