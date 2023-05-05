using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Entities.DTO.FixedAsset
{
    //Thông tin bộ lọc theo id
    public class FixedAssetIdFilter
    {
        /// <summary>
        /// Danh sách id chứa trong danh sách tài sản
        /// </summary>
        public List<string> inEntityIds { get; set; }

        /// <summary>
        /// Danh sách id không chứa trong danh sách tài sản
        /// </summary>
        public List<string> notInEntityIds { get; set; }
    }
}
