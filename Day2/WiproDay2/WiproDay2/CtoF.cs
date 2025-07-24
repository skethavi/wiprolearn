using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay2
{
    internal class CtoF
    {
        public double CeltoFah(double celcius)
        {
            double f = ((9 * celcius) / 5) + 32;
            return f;
        }
        static void Main()
        {
            double celcius;
            Console.WriteLine("enter celcius");
            celcius=Convert.ToDouble(Console.ReadLine());
            CtoF obj = new CtoF();
            Console.WriteLine("fahrenheit is" + obj.CeltoFah(celcius));
        }
    }
}
