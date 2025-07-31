using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    internal class ReflectionEx4
    {
        [Obsolete("Deprecated Method", false)]
        public static void TestMethod()
        {
            Console.WriteLine("Demo Method...");
        }
        static void Main()
        {

        }
    }
}
