using Demo.WebApplication.API.lib;
using Demo.WebApplication.BL.BaseBL;
using Demo.WebApplication.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApplication.API.Controllers
{ 
    public class FixedAssetCategoriesController : BaseController<FixedAssetCategory>
    {
        public FixedAssetCategoriesController(IBaseBL<FixedAssetCategory> baseBL) : base(baseBL)
        {
        }
    }
}
