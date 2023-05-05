using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Entities
{
    public class FixedAssetIncrementDetail
    {
        /// <summary>
        /// Id chi tiết ghi tăng tài sản 
        /// </summary>
        public string? fixed_asset_increment_detail_id { get; set; }

        /// <summary>
        /// Id ghi tăng tài sản
        /// </summary>
        public string? fixed_asset_increment_id { get; set; }

        /// <summary>
        /// Id tài sản
        /// </summary>
        public string? fixed_asset_id { get; set; }
    }
}
