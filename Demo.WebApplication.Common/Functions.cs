using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common
{
    public class Functions
    {
        /// <summary>
        /// Chuyển đổi ten viết hoa thành tên gạch dưới 
        /// VD: FullName => full_name
        /// </summary>
        /// <param name="name">tên muốn đổi</param>
        /// <returns>trả về tên cách nhau bằng dấu gạch dưới</returns>
        public static string ConvertPascalCaseToUnderscore(string name)
        {
            var result = Regex.Replace(name, "(?<=[a-z0-9])[A-Z]", m => "_" + m.Value);
            return result.ToLowerInvariant();
        }

        /// <summary>
        /// Lấy giá trị property bằng chuỗi
        /// </summary>
        /// <param name="src">Đối tượng muốn lấy property</param>
        /// <param name="propName">Tên muốn lấy</param>
        /// <returns>trả về giá trị của property</returns>
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
