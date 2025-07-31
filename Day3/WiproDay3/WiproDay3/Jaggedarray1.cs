using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay3
{
    internal class Jaggedarray1
    {
        static void Main()
        {
            int[][] jaggedarray = new int[2][];
            int[] x = new int[] {2,3,4};
            int[] y = new int[] {2,4,5};
            jaggedarray[0]=x; 
            jaggedarray[1]=y;

            Console.WriteLine(jaggedarray[0][0]);
            Console.WriteLine(jaggedarray[0][1]);
            Console.WriteLine(jaggedarray[0][2]);

            Console.WriteLine(jaggedarray[1][0]);
            Console.WriteLine(jaggedarray[1][1]);
            Console.WriteLine(jaggedarray[1][2]);
        }
    }
}
