
using Demo.WebApplication.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;

namespace Demo.WebApplication.API.Controllers
{
    /// <summary>
    /// Nguyễn Ngọc đức
    /// </summary>
    [Route("api/employees")]//attribute
    [ApiController]
    public class EmployeesController : ControllerBase//ke thua 
    {
        [HttpGet("TEST")]
        public string TEST([FromBody] List<string> strings)
        {
            return string.Join(",",strings);
        }

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
        /// Thêm nhân viên mới
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            List<string> val = new List<string>();
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(employee);

            bool isValid = Validator.TryValidateObject(employee, validationContext, validationResults, true);
            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {

                    val.Add(validationResult.ErrorMessage ?? string.Empty);
                }
            }

            return StatusCode(400, new
            {
                Code = 1,

                Message = val,
                Data = employee.EmployeeName
            });
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] List<string> ids)
        {
            string combinedString = string.Join(",", ids);
            return StatusCode(200, combinedString);
        }

        [HttpDelete("{entityID}")]
        public IActionResult Delete(string entityID )
        {
            return StatusCode(200, entityID);
        }
    }
}
