using Demo.WebApplication.BL.BaseBL;
using Demo.WebApplication.BL.FixedAssetBL;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.DL.BaseDL;
using Demo.WebApplication.DL.FixedAssetDL;
using Demo.WebApplication.DL.FixedAssetIncrementDetailDL;
using Demo.WebApplication.DL.FixedAssetIncrementDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.BL.FixedAssetIncrementDetailBL
{
    public class FixedAssetIncrementDetailBL : BaseBL<FixedAssetIncrementDetail>, IFixedAssetIncrementDetailBL
    {
        public FixedAssetIncrementDetailBL(IFixedAssetIncrementDetailDL fixedAssetIncrementDetailDL) : base(fixedAssetIncrementDetailDL)
        {
        }
    }
}
