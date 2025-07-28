using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopsExample
{
    static class Demo1
    {
        public static void Show()
        {
            Console.WriteLine("Show Method from Demo Class...");
        }

        public static string Trainer()
        {
            return "Trainer is Prasanna P";
        }
    }


    internal class StaticClass
    {
        static void Main()
        {
            Demo1.Show();
            Console.WriteLine(Demo1.Trainer());
        }
    }

}
