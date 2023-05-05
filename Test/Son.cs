using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Son : Father
    {
        public string old;
        public const string tt = "I am {0}";

        public Son(string name) : base(name)
        {
        }

        X MyFunction<X, Y>(X x, Y y)
        {
            return x;
        }
        public override void B()
        {
            base.Name = "a";
            Console.WriteLine(Name);
        }
    }
}
