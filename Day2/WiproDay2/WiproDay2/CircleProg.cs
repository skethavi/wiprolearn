using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay2
{
    internal class CircleProg
    {
        public void Calc(double r)
        {
            double area, circ;
            area = Math.PI*r*r;
            circ = 2 * Math.PI * r;
            Console.WriteLine("area of circle"+area);
            Console.WriteLine("circumference of circle "+circ);
        }
        static void Main()
        {
            double r;
            Console.WriteLine("enter the radius");
            r=Convert.ToDouble(Console.ReadLine());
            CircleProg obj = new CircleProg();
            obj.Calc(r);

        }
        
    }
}
