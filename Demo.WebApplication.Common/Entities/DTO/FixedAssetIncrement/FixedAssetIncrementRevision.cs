using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Entities.DTO.FixedAssetIncrement
{
    /// <summary>
    /// Thông tin các trường của tài sản ghi tăng (insert, update)
    /// </summary>
    public class FixedAssetIncrementRevision:Entities.FixedAssetIncrement
    {
        /// <summary>
        /// Danh sách chứa các id của tài sản
        /// </summary>
        public List<string>? fixed_asset_id { get; set; }
    }
}
