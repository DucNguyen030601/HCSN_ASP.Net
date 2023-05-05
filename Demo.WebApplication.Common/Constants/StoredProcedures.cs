using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Constants
{
    public static class StoredProcedures
    {
        /// <summary>
        /// Procedure thêm mới bản ghi
        /// </summary>
        public const string Insert = "Proc_{0}_Insert";

        /// <summary>
        /// Procedure thêm nhiều bản ghi
        /// </summary>
        public const string InsertMultiple = "Proc_{0}_InsertMultiple";

        /// <summary>
        /// Procedure cập nhật bản ghi
        /// </summary>
        public const string Update = "Proc_{0}_Update";

        /// <summary>
        /// Procedure sửa nhiều bản ghi
        /// </summary>
        public const string UpdateMultiple = "Proc_{0}_UpdateMultiple";

        /// <summary>
        /// Procedure xoá bản ghi
        /// </summary>
        public const string Delete = "Proc_{0}_Delete";

        /// <summary>
        /// Procedure xoá nhiều bản ghi
        /// </summary>
        public const string DeleteMultiple = "Proc_{0}_DeleteMultiple";

        /// <summary>
        /// Procedure lấy bản ghi theo id
        /// </summary>
        public const string GetById = "Proc_GetById";
        
        /// <summary>
        /// Procedure lấy mã code mới
        /// </summary>
        public const string GetNewCode = "Proc_GetNewCode";

        /// <summary>
        /// Procedure lấy code
        /// </summary>
        public const string GetCode = "Proc_GetCode";

        /// <summary>
        /// Procedure số bản ghi trùng mã
        /// </summary>
        public const string DuplicateCode = "Proc_DuplicateCode";
        
        /// <summary>
        /// Procedure lấy tất cả bản ghi, tổng số trang, tổng số bản ghi
        /// </summary>
        public const string GetPaging = "Proc_GetPaging";

        /// <summary>
        /// Procedure lấy thêm thông tin phân trang (VD: tổng số tiền,..)
        /// </summary>
        public const string MorePagingInfor = "Proc_{0}_MorePagingInfor";

        /// <summary>
        /// Procedure lấy danh sách bản ghi theo danh sách id
        /// </summary>
        public const string GetRecordsByIds = "Proc_{0}_GetById";
    }
}
