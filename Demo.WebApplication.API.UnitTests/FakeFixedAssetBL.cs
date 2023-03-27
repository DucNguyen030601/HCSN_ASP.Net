using Demo.WebApplication.BL.FixedAssetBL;
using Demo.WebApplication.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.API.UnitTests
{
    internal class FakeFixedAssetBL : IFixedAssetBL
    {
        public List<FixedAsset> GetFixedAssetList()
        {
            throw new NotImplementedException();
        }

        public int InsertFixedAsset(FixedAsset fixedAsset)
        {
            return 1;
        }
    }
}
