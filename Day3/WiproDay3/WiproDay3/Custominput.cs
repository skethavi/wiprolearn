using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay3
{
    internal class Custominput
    {
        static void Main()
        {
            int n, m;
            Console.WriteLine("Enter the row and coloum number");
            n=Convert.ToInt32(Console.ReadLine());
            m=Convert.ToInt32(Console.ReadLine());
            int[,] x = new int[n, m];

            Console.WriteLine("enter the array elementstotal{0}",n*m);
            for(int i=0; i<x.GetLength(0); i++)
            {
                for(int j = 0; j < x.GetLength(n - 1); j++)
                {
                    x[i,j] =Convert.ToInt32(Console.ReadLine());
                }
            }
            Console.WriteLine("display elements in matrix formet");
            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    Console.Write(x[i, j]+" ");
                }
                Console.WriteLine();
            }
            
        }
        
        
    }
}
