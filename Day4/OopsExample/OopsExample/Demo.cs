using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopsExample
{
    internal class Demo
    {
        static int count;
        public void Increment()
        {
            count++;
        }
        public void Show()
        {
            Console.WriteLine("count: "+count);
        }
        static void Main(string[] args)
        {
            Demo demo1 = new Demo();
            Demo demo2 = new Demo();
            demo1.Increment();
            demo1.Show();
            demo2.Increment();
            demo2.Show();
        }
    }
}
