using Demo.WebApplication.API.lib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixedAssetCategoriesController : ControllerBase
    {
        DBConfig db = new DBConfig();

        [HttpGet]
        public IActionResult GetFixedAssetCategories()
        {
            string sql = $"select * from fixed_asset_category";
            var data = db.FixedAssetCategores(sql);
            return StatusCode(200, data);
        }
    }
}
