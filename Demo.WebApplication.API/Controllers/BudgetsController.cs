using Demo.WebApplication.BL.BaseBL;
using Demo.WebApplication.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApplication.API.Controllers
{
    public class BudgetsController : BaseController<Budget>
    {
        public BudgetsController(IBaseBL<Budget> baseBL) : base(baseBL)
        {
        }
    }
}
