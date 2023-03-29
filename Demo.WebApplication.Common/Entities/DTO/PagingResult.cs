namespace Demo.WebApplication.Common.Entities.DTO
{
    /// <summary>
    /// Thông tin phân trang
    /// </summary>
    public class PagingResult
    {
        /// <summary>
        /// Dữ liệu kết quả trả về
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// Số lượng dữ liệu trả về
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// Số lượng trang
        /// </summary>
        public int TotalPage { get; set; }

        public object? MoreInfo { get; set; }
    }
}