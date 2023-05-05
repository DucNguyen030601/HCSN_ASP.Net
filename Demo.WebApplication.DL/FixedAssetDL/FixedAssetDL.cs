using Dapper;
using Demo.WebApplication.Common.Constants;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Entities.DTO.FixedAsset;
using Demo.WebApplication.DL.BaseDL;
using Demo.WebApplication.DL.DBConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Demo.WebApplication.DL.FixedAssetDL
{
    public class FixedAssetDL : BaseDL<FixedAsset>, IFixedAssetDL
    {
        public FixedAssetDL(IStorage storage) : base(storage)
        {
        }

    }
}
