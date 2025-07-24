using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay2
{
    internal class Maxof3
    {
        public void check(int a, int b, int c)
        {
            int m = a;
            if (m < b)
            {
                m = b;
            }
            if (m < c)
            {
                m = c;
            }
            Console.WriteLine("the max number is"+m);
        }
        static void Main()
        {
            int a, b, c;
            Console.WriteLine("enter the numbers");
            a=Convert.ToInt32(Console.ReadLine());
            b=Convert.ToInt32(Console.ReadLine());
            c=Convert.ToInt32(Console.ReadLine());
            Maxof3 obj=new Maxof3();
            obj.check(a, b, c);
        }
    }
}
