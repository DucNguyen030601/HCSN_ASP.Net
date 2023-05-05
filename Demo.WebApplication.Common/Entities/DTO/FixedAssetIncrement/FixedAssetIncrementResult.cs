using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Entities.DTO.FixedAssetIncrement
{
    public class FixedAssetIncrementResult
    {
        /// <summary>
        /// Id ghi tăng tài sản
        /// </summary>
        public string fixed_asset_increment_id { get; set; }

        /// <summary>
        /// Mã ghi tăng tài sản
        /// </summary>
        public string fixed_asset_increment_code { get; set; }

        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        public DateTime increment_date { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        public DateTime production_year { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal cost { get; set; }
    }
}
