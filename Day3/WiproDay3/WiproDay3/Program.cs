using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] x=new int[2,3]
            {
                { 2,4,6},
                { 3,5,7}
            };
            for(int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    Console.WriteLine(x[i,j]+" ");
                }

            }
            Console.WriteLine();
        }
    }
}
