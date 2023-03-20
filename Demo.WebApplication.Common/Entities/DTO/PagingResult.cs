namespace Demo.WebApplication.Common.Entities.DTO
{
    /// <summary>
    /// Thông tin phân trang
    /// </summary>
    public class PagingResult<T>
    {
        /// <summary>
        /// Dữ liệu kết quả trả về
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// Số lượng dữ liệu trả về
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// Số lượng trang
        /// </summary>
        public int TotalPage { get; set; }
    }
}