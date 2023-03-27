using Demo.WebApplication.Common.Enums;

namespace Demo.WebApplication.Common.Entities.DTO
{
    public class ErrorResult
    {
        /// <summary>
        /// thông tin mã code
        /// </summary>
        public ErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Thông tin xử lý bên phía backend hoặc fontend
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông tin bên phía người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Chi tiết thông tin
        /// </summary>
        public object MoreInfo { get; set; }

        /// <summary>
        /// Thông tin mã khi thực hiện 1 yêu cầu, giao dịch nào đó như 
        /// </summary>
        public string TraceId { get; set; }

    }
}
