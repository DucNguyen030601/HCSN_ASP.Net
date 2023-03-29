using Dapper;
using Demo.WebApplication.Common.Constants;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.DL.DBConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.BL.BaseBL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Hàm thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created by: NNduc(21/03/2023)
        ServiceResult InsertRecord(T record);

        /// <summary>
        /// Hàm sửa 1 bản ghi
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1: Nếu update thành công
        /// 0: Nếu delete thất bại
        /// </returns>
        /// Created by: NNduc(21/03/2023)
        ServiceResult UpdateRecord(string id,T record);

        /// <summary>
        /// Hàm xoá 1 bản ghi
        /// </summary>
        /// <param name="entityId">Mã id của thực thể cần xoá</param>
        /// <returns>
        /// Số bản ghi bị ảnh hưởng
        /// </returns>
        ServiceResult DeleteRecord(string entityId);

        /// <summary>
        /// Lấy danh sách tất cả các bản ghi
        /// </summary>
        /// <returns>Danh sách</returns>
        List<T> GetRecords();

        /// <summary>
        /// Hàm lấy ra mã code mới
        /// </summary>
        /// <returns>Mã code cần lấy</returns>
        string GetNewCode();

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id">id bản ghi muốn lấy</param>
        /// <returns>Bản ghi thoả mãn điều kiện</returns>
        public T GetRecordsById(string id);

        /// <summary>
        /// Lấy danh sách bản ghi và tổng số bản ghi có phân trang
        /// </summary>
        /// <param name="filter">bộ lọc tìm theo tên, hoặc mã</param>
        /// <param name="page">trang hiện tại</param>
        /// <param name="pageSize">số lượng bản ghi trong 1 trang</param>
        /// <param name="sort">sắp xếp</param>
        /// <returns>Trả về thông tin danh sách bản ghi và tổng số bản ghi có phân trang</returns>
        public PagingResult GetPagingResult(int? page = 1, int? pageSize = 10,string? where="", string? sort = "");
    }
}
