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

        /// <summary>
        /// Lấy mã code đầu cộng thêm 1 (ABC001 -> ABC002)
        /// </summary>
        /// <param name="s">Mã code đầu</param>
        /// <returns>Mã code mới</returns>
        public static string GetNewCode(string s,int length = 50)
        {
            int count = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(s[i])) count++;
                else break;
            }
            string firstString = s.Substring(0, s.Length - count);
            if (count == 0)
            {
                return firstString + 0;
            }
            else
            {
                string lastString = s.Substring(s.Length - count);
                int convertNumber = int.Parse(lastString) + 1;

                //Lấy số lượng chữ số sau khi cộng 1
                //lastString = 01 -> convert number = 2
                // 09 -> 10 -> 2; 99 -> 100 -> 3
                //Lấy số lượng => 2 
                int numberOfChar = lastString.Length >= convertNumber.ToString().Length ? lastString.Length : convertNumber.ToString().Length;

                //Thêm số 0 và đầu nếu số trước là 1 -> 001
                string newCode = firstString + convertNumber.ToString($"D{numberOfChar}");
                //Nếu độ dài chuỗi dài hơn cho phép ta thực hiện cắt chuỗi 
                return newCode.Length > length ? newCode.Substring(1) : newCode;
            }
        }
    }
}
