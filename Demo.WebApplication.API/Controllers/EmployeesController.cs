
using Demo.WebApplication.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApplication.API.Controllers
{
    [Route("api/employees")]//attribute
    [ApiController]
    public class EmployeesController : ControllerBase//ke thua 
    {

        /// <summary>
        /// lấy mã nhân viên
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("{employeeId}")] //truyền tham số dấu ngoặc nhọn
        public Employee GetEmployeeName([FromRoute] Guid employeeId)
        {
            return new Employee
            {
                EmployeeId = employeeId,
                EmployeeCode = "NV05",
                EmployeeName = "Nguyễn Ngọc Đức",
            };
        }

        [HttpGet]
        [Route("employeeId")]
        public IActionResult GetPaging(
            [FromQuery] string? keyword,
            [FromQuery] Guid? departmentId,
            [FromQuery] Guid? positionId,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNmuber = 1)
        {
            return StatusCode(200, new List<Employee> {  new Employee
            {
                EmployeeId = Guid.Empty,
                EmployeeCode="NV05",
                EmployeeName = "Nguyễn Ngọc Đức",
            },
             new Employee
            {
                EmployeeId = Guid.Empty,
                EmployeeCode="NV05",
                EmployeeName = "Nguyễn Ngọc Đức",
            } });
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            return StatusCode(400, new
            {
                Code = 1,
                Message = "Trùng mã nhân viên"
            });
        }
    }
}
