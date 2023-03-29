﻿using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.DL.BaseDL
{
    public interface IBaseDL<T>
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
        int InsertRecord(T record);

        /// <summary>
        /// Hàm sửa 1 bản ghi
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1: Nếu update thành công
        /// 0: Nếu delete thất bại
        /// </returns>
        /// Created by: NNduc(21/03/2023)
        int UpdateRecord(string id,T record);

       /// <summary>
       /// Hàm xoá 1 bản ghi
       /// </summary>
       /// <param name="entityId">Mã id của thực thể cần xoá</param>
       /// <returns>
       /// Số bản ghi bị ảnh hưởng
       /// </returns>
        int DeleteRecord(string entityId);

        /// <summary>
        /// Kiểm tra mã tồn tại hay chưa
        /// </summary>
        /// <param name="code">Mã muốn kiểm tra</param>
        /// <param name="state">Trạng thái của </param>
        /// <returns>
        /// True -> có tồn tại
        /// False -> không tồn tại 
        /// </returns>
        bool IsCodeExit(string code, string? id = null);

        /// <summary>
        /// Lấy danh sách tất cả các bản ghi
        /// </summary>
        /// <returns>Danh sách</returns>
        List<T> GetRecords();

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id">id bản ghi muốn lấy</param>
        /// <returns>Bản ghi thoả mãn điều kiện</returns>
        T GetRecordsById(string id);

        /// <summary>
        /// Hàm lấy ra mã code mới
        /// </summary>
        /// <returns>Mã code cần lấy</returns>
        string GetNewCode();

        /// <summary>
        /// Lấy danh sách bản ghi và tổng số bản ghi có phân trang
        /// </summary>
        /// <param name="filter">bộ lọc tìm theo tên, hoặc mã</param>
        /// <param name="page">trang hiện tại</param>
        /// <param name="pageSize">số lượng bản ghi trong 1 trang</param>
        /// <param name="sort">sắp xếp</param>
        /// <returns>Trả về thông tin danh sách bản ghi và tổng số bản ghi có phân trang</returns>
        PagingResult GetPagingResult( int? page = 1, int? pageSize = 10,string? where="",string? sort="");
    }
}
