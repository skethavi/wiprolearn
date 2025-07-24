using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Program to Perform Calculation of 2 Numbers 
/// Accept 2 Numbers 
/// Write methods for business Logic
/// Call them in main method
/// </summary>

namespace WiproDay2
{
    internal class Calculation
    {
        public int Sum(int x, int y)
        {
            return x + y;
        }
        public int Sub(int x,int y) 
        { 
            return x - y; 
        }
        public int Mul(int x, int y)
        {
            return x * y;
        }
        static void Main()
        {
            int x, y;
            Console.WriteLine("enter the numbers");
            x=Convert.ToInt32(Console.ReadLine());
            y=Convert.ToInt32(Console.ReadLine());
            Calculation calculation = new Calculation();
            int result = calculation.Sum(x,y);
            Console.WriteLine("sum is"+result);
            result = calculation.Sub(x,y);
            Console.WriteLine("sub is" + result);
            result = calculation.Mul(x,y);
            Console.WriteLine("mult is" + result);
        }


    }
}
