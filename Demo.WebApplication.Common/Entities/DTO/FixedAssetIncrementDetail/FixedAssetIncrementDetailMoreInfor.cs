using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Entities.DTO.FixedAssetIncrementDetail
{
    public class FixedAssetIncrementDetailMoreInfor
    {
        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal total_cost { get; set; }

        /// <summary>
        /// Tổng số Hao hòn, khấu hao
        /// </summary>
        public decimal total_accumulated_depreciation { get; set; }

        /// <summary>
        /// Tổng số giá trị còn lại
        /// </summary>
        public decimal total_residual_value { get; set; }
    }
}
