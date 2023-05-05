using Demo.WebApplication.Common.Entities.DTO.FixedAssetIncrementDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Entities.DTO.FixedAsset
{
    /// <summary>
    /// Thông tin thêm về Tài sản
    /// </summary>
    public class FixedAssetMoreInfor : FixedAssetIncrementDetailMoreInfor
    {
        /// <summary>
        /// Tổng số lượng
        /// </summary>
        public int total_quantity { get; set; }
    }
}
