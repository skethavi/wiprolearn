using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay2
{
    internal class PosNeg
    {
        public void Check(int n)
        {
            if (n > 0)
            {
                Console.WriteLine(n + "is a positive number");
            }
            else
            {
                Console.WriteLine(n + " is a negative number");
            }
        }
        static void Main()
        {
            int n;
            Console.WriteLine("enter the number");
            n=Convert.ToInt32(Console.ReadLine());
            PosNeg obj = new PosNeg();  
            obj.Check(n);
        }
    }
}
