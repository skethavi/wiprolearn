using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay2
{
    internal class Loopex
    {
        public void Show()
        {
            int i=0;
            int count = 10;
            while(i < count)
            {
                Console.WriteLine("welcome");
                i++;
            }
        }
        static void Main()
        {
            Loopex obj = new Loopex();
            obj.Show();
        }
    }
}
