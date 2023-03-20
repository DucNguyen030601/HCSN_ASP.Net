using Demo.WebApplication.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.BL.FixedAssetBL
{
    public interface IFixedAssetBL
    {
        /// <summary>
        /// Hàm thêm mới tài sản
        /// </summary>
        /// <param name="fixedAsset">đối tượng muốn thêm</param>
        /// <returns>trả về 1: là thêm thành công
        /// trả về 2: thất bại
        /// </returns>
        int InsertFixedAsset(FixedAsset fixedAsset);

        /// <summary>
        /// Hàm lấy danh sách bản ghi từ DB
        /// </summary>
        /// <returns>danh sách bản ghi cần lấy</returns>
        List<FixedAsset> GetFixedAssetList();
    }
}
