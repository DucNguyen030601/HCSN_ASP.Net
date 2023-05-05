using Dapper;
using Demo.WebApplication.Common.Constants;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO.FixedAsset;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.DL.BaseDL;
using Demo.WebApplication.DL.DBConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.WebApplication.Common.Entities.DTO.FixedAssetIncrementDetail;

namespace Demo.WebApplication.DL.FixedAssetIncrementDetailDL
{
    public class FixedAssetIncrementDetailDL : BaseDL<FixedAssetIncrementDetail>, IFixedAssetIncrementDetailDL
    {
        public FixedAssetIncrementDetailDL(IStorage storage) : base(storage)
        {
        }

    }
}
