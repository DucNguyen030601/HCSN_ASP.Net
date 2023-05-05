using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }
        static void Main(string[] args)
        {
            /*Console.WriteLine(string.Format(Son.tt, "BATMAN"));
            Son son = new Son("a");
            DateTime dtToday = new System.DateTime(2012, 4, 2);
            DateTime dtMonthBefore = new System.DateTime(2012, 5, 2);
            TimeSpan diffResult = dtToday.Subtract(dtMonthBefore);
            Console.WriteLine(diffResult.TotalDays);*/

            //son.A("x");
            /*      var a = new
                  {
                      Name = "Nguyễn Ngọc Đức"
                  };

                  Console.WriteLine(a.GetType().GetProperty("Name").GetValue(a, null));*/

            /*  string sort = "-modifiedDate";
              var paramters = test(sort);
              foreach (var arg in paramters)
              {
                  Console.WriteLine(arg.Key + " - " + arg.Value);
              }*/

            /*    List<dynamic> list = new List<dynamic>
                    {
                        new { Name = "Alice", Age = 25 },
                        new { Name = "Bob", Gender = "male" },
                        new { Name = "Charlie", Age = 30, Gender = "male" }
                    };

                string property = "Age";


                foreach (var item in list)
                {
                    if (item.GetType() == typeof(Dictionary<string, object>) && ((Dictionary<string, object>)item).ContainsKey(property))
                    {
                        Console.WriteLine($"{item.Name} has {property}: {((Dictionary<string, object>)item)[property]}");
                    }
                    else
                    {
                        Console.WriteLine($"{item.Name} does not have {property}");
                    }
                }*/
            //Check(null);
            /*CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            var a = 1456.ToString("N").Replace(",", ".").Replace(".00", "").Replace(",", ".");
            Console.WriteLine(a);*/
            //Console.WriteLine(GetNewCode());
            Console.ReadKey();
        }

 

        static void Check(int? s = 5)
        {
            if (s == null)
            {
                Console.WriteLine("This is null");
            }
            if (s == 5)
            {
                Console.WriteLine("This is empty");
            }

        }

        static Dictionary<string, string> test(string sort)
        {
            Dictionary<string, string> paramter = new Dictionary<string, string>();
            var sorts = sort.Split(',');
            foreach (string s in sorts)
            {
                if (IsCheckParameter(s))
                {
                    paramter.Add(s.Substring(0, 1) == "-" ? "DESC" : "ASC", s.Substring(1));
                }
                else
                {
                    paramter.Clear();
                    break;
                }
            }
            return paramter;
        }
        static bool IsCheckParameter(string s)
        {
            int count = 0;
            if (!s.StartsWith("-") && !s.StartsWith("+")) return false;

            foreach (char c in s)
            {
                if (c == '-' || c == '+') count++;
            }
            return count == 1;
        }

        static string GetNewCode(string s = "AB01C9",int length = 6)
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
                return newCode.Length > length ? newCode.Substring(1) : newCode;
            }
        }
    }
}
