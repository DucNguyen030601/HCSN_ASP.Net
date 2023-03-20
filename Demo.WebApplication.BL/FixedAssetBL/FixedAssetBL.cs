using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.DL.DBConfig;
using Demo.WebApplication.DL.FixedAssetDL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.BL.FixedAssetBL
{
    public class FixedAssetBL : IFixedAssetBL
    {
        #region Field

        private IFixedAssetDL _fixedAssetDL;

        #endregion

        #region Constuctor

        public FixedAssetBL(IFixedAssetDL fixedAssetDL) 
        {
            _fixedAssetDL = fixedAssetDL;
        }
        public List<FixedAsset> GetFixedAssetList()
        {
            List<FixedAsset>fixedAssets = _fixedAssetDL.GetFixedAssetList();

            return fixedAssets;
        }

        #endregion

        public int InsertFixedAsset(FixedAsset fixedAsset)
        {
            var validateResult = ValidateFixedAsset(fixedAsset);

            var numberOfAffectedRows = _fixedAssetDL.InsertFixedAsset(fixedAsset);

            return numberOfAffectedRows;
        }

        /// <summary>
        ///  Hàm kiểm tra validate
        /// </summary>
        /// <param name="fixedAsset"></param>
        /// <returns></returns>
        private ValidateResult ValidateFixedAsset(FixedAsset fixedAsset)
        {
            //Triển khai thân hàm

            return new ValidateResult
            {
                IsSucess = true
            };
        }


    }
}
