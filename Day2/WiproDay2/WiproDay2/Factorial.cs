using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay2
{
    internal class Factorial
    {
        public void Fact(int n)
        {
            int i = 1, f = 1;
            while (i <= n)
            {
                f=i*f; 
                i++;
            }
            Console.WriteLine("factorial of n is "+f);
        }
        static void Main()
        {
            int n;
            Console.WriteLine("enter the number");
            n = Convert.ToInt32(Console.ReadLine());
            Factorial obj = new Factorial();
            obj.Fact(n);
        }
    }
}
