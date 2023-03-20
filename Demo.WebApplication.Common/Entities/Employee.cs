using Demo.WebApplication.Common.Enums;

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

        public Guid EmployeeId { get; set; }    
        public string EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreateBy { get; set; }

    }
}
