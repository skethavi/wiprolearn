using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    internal class Bitwise
    {
        static void Main()
        {
            int a = 5, b = 3;
            //101 and
            //011
            //001
            Console.WriteLine(a&b);
            Console.WriteLine(b&a);
            //101 or
            //011
            //111
            Console.WriteLine(a|b);
            //101 xor
            //011
            //110
            Console.WriteLine(a^b);
            //negation ~
            Console.WriteLine(~a);

        }
    }
}
