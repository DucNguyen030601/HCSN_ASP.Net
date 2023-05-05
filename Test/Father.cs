using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    abstract class Father
    {
        protected string Name;
      
        public Father(string name)
        {
            Console.WriteLine("abc123456");
            Name = name;
        }

        public  void A(string x)
        {
            Console.WriteLine("Hàm A của lớp cha");
        }
        public abstract void B();
       

    }
}
