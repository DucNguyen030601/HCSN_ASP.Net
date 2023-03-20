using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Entities.DTO
{
    public class ValidateResult
    {
        /// <summary>
        /// Kết quả validate: true là không có lỗi, ngược lại
        /// </summary>
        public bool IsSucess { get; set; }
    }
}
