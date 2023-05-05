using Demo.WebApplication.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Entities.DTO
{
    public class ServiceResult
    {
        /// <summary>
        /// Thông tin thành công hay không
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Thông tin mã code, sẽ cho là NUllable nếu giá trị IsSuccess thành công
        /// </summary>
        public ErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Mô tả thông tin rõ hơn có thể là string, mảng
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Thông tin khi rõ hơn cho Dev
        /// </summary>
        public string Message { get; set; }
    }
}
