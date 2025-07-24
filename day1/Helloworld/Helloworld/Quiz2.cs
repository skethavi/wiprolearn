using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    internal class Quiz2
    {
        static void Main()
        {
            Console.WriteLine("5" + 3 + 8);
            Console.WriteLine("5" + (3 + 8)); 
            Console.WriteLine("5 + 3" + 8);
            int x = 12;
            Console.WriteLine(x++ + x-- + --x + ++x + x++ + --x);
            // 12 + 13 + 11 + 12 + 12 + 12
        }
    }
}
