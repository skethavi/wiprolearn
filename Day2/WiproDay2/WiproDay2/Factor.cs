using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay2
{
    internal class Factor
    {
        public void Check(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                if (n % i == 0)
                {
                    Console.WriteLine("factors"+i);
                }
            }
        }
        static void Main()
        {
            int n;
            Console.WriteLine("enter the number");
            n = Convert.ToInt32(Console.ReadLine());
            Factor obj = new Factor();
            obj.Check(n);
        }
    }
}
