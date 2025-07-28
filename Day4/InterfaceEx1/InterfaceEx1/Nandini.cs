using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceEx1
{
    internal class Nandini : ITraining
    {
        public void Email()
        {
            Console.WriteLine("Email is Nandini@gmail.com");
        }

        public void Name()
        {
            Console.WriteLine("Name is Nandini...");
        }
    }
}
