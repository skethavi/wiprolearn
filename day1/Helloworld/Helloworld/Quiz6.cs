using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    internal class Quiz6
    {
        int x;
        static void Main()
        {
            Quiz6 obj1 = new Quiz6();
            obj1.x = 12;
            Quiz6 obj2 = obj1;
            obj2.x = 13;
            Console.WriteLine(obj1.x);
            //display the memory location
            Console.WriteLine(obj1.GetHashCode());
            Console.WriteLine(obj2.GetHashCode());
        }
    }
}
