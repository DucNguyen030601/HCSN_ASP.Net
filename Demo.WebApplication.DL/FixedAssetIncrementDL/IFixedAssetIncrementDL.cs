using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO.FixedAssetIncrement;
using Demo.WebApplication.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.DL.FixedAssetIncrementDL
{
    public interface IFixedAssetIncrementDL : IBaseDL<FixedAssetIncrement>
    {
        /// <summary>
        /// Hàm thêm mới 1 bản ghi chi tiết
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created by: NNduc(21/03/2023)
        public bool InsertRecord(FixedAssetIncrementRevision record);

        /// <summary>
        /// Hàm sửa 1 bản ghi chi tiết
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created by: NNduc(21/03/2023)
        public bool UpdateRecord(string id,FixedAssetIncrementRevision record);
    }
}
