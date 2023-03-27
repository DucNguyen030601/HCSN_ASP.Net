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
using Demo.WebApplication.BL.BaseBL;
using Demo.WebApplication.DL.BaseDL;

namespace Demo.WebApplication.BL.FixedAssetBL
{
    public class FixedAssetBL : BaseBL<FixedAsset>,IFixedAssetBL
    {
        public FixedAssetBL(IFixedAssetDL fixedAssetDL) : base(fixedAssetDL)
        {

        }
        /// <summary>
        /// Hàm xử lý validate không dùng chung nếu muốn validata thay đổi
        /// thì các lớp con kế thừa và thay đổi lại
        /// </summary>
        /// <param name="record">Bản ghi muốn validate</param>
        /// <returns>Danh sách chuỗi bị lỗi</returns>
        protected override List<string> ValidateCustom(FixedAsset record)
        {
            return new List<string>();
        }


    }
}
