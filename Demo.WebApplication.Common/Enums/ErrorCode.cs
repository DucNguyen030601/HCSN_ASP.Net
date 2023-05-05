namespace Demo.WebApplication.Common.Enums
{
    public enum ErrorCode
    {
        /// <summary>
        /// Ngooại lệ
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Xác thực dữ liệu
        /// </summary>
        InvalidData = 2,

        /// <summary>
        /// Trùng mã
        /// </summary>
        DuplicateCode=3,

        /// <summary>
        /// Lỗi không thêm được
        /// </summary>
        InsertFailed = 4,

        /// <summary>
        /// Lỗi không sửa được
        /// </summary>
        UpdateFailed = 5,

        /// <summary>
        /// Lỗi không xoá được
        /// </summary>
        DeleteFailed = 6,
        
        /// <summary>
        /// Lỗi xoá phát sinh
        /// </summary>
        DeleteArised = 7
    }
}
