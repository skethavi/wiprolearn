using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helloworld
{
    internal class Bitwise1
    {
        static void Main()
        {
            int a = 5, b = 3;
            //and
            Console.WriteLine(a & b);
            //or
            Console.WriteLine(a | b);
            //xor
            Console.WriteLine(a ^ b);
            //not
            Console.WriteLine(~a);
            Console.WriteLine(~7);
            Console.WriteLine(~-3);
        }
    }
}
