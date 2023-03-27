using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Son : Father
    {
        public const string tt = "I am {0}"; 
        public Son(string name) : base(name)
        {

        }
        public Son()
        {

        }
        public void A(string x, string y)
        {
            Console.WriteLine("AAAA");
            base.A(x);
        }

        public override void B()
        {
            throw new NotImplementedException();
        }
    }
}
