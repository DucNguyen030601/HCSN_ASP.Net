using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine(string.Format(Son.tt, "BATMAN"));
            Son son = new Son();
            son.A("x");*/
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

            var l = new List<object>();
            l.Add(new
            {
                a = 1 
            });
            Check(null);
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
                    paramter.Add(s.Substring(0, 1) == "-"?"DESC":"ASC", s.Substring(1));
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
            if(!s.StartsWith("-")&&!s.StartsWith("+")) return false;

            foreach(char c in s)
            {
                if (c == '-' || c == '+') count++;
            }
            return count==1;
        }
    }
}
