using Demo.WebApplication.API.lib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        DBConfig db = new DBConfig();
        [HttpGet]
        public IActionResult GetDepartments()
        {
            string sql = $"select * from department";
            var data = db.Departments(sql);
            return StatusCode(200, data);
        }
    }
}
