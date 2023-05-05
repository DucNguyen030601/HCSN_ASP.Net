using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Entities.DTO.FixedAsset
{
    /// <summary>
    /// Thông tin Tài sản
    /// </summary>
    public class FixedAssetResult
    {
        /// <summary>
        /// Id tài sản
        /// </summary>
        public string? fixed_asset_id { get; set; }

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
        /// Tên loại tài sản
        /// </summary>
        public string? fixed_asset_category_name { get; set; }
        
        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// Hao mòn, khấu hao luỹ kế
        /// </summary>
        public decimal accumulated_depreciation { get; set; }

        /// <summary>
        /// giá trị còn lại
        /// </summary>
        public decimal residual_value { get; set; }

        /// <summary>
        /// Sử dụng
        /// </summary>
        public bool active { get; set; }

        /// <summary>
        /// mã ghi tăng
        /// </summary>
        public string? fixed_asset_increment_code { get; set; }
    }
}
