using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Entities.DTO.FixedAssetIncrementDetail
{
    public class FixedAssetIncrementDetailResult : Entities.FixedAssetIncrementDetail
    {
        /// <summary>
        /// Mã ghi tăng tài sản
        /// </summary>
        public string? fixed_asset_increment_code { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string? fixed_asset_code { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string? fixed_asset_name { get; set; }

        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        public string? department_name { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal cost { get; set; }

        /// <summary>
        /// Hao mòn, khấu hao luỹ kế
        /// </summary>
        public decimal accumulated_depreciation { get; set; }

        /// <summary>
        /// giá trị còn lại
        /// </summary>
        public decimal residual_value { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime modified_date { get; set; }


    }
}
