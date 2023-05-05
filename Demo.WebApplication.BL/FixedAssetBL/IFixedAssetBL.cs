using Demo.WebApplication.BL.BaseBL;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.BL.FixedAssetBL
{
    public interface IFixedAssetBL:IBaseBL<FixedAsset>
    {
        MemoryStream ExportExcel(string? sort = "",string? where=""); 
    }
}
