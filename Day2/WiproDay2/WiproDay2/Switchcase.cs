using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay2
{
    internal class Switchcase
    {
        public void Check(char choice)
        {
            switch (choice)
            {
                case 'a':
                case 'A':
                case '1':
                    Console.WriteLine("Hi I am Rajesh");
                    break;
                case 'b':
                case 'B':
                case '2':
                    Console.WriteLine("Hi I am Yamini...");
                    break;
                case 'c':
                case 'C':
                case '3':
                    Console.WriteLine("Hi I am Sreeja...");
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }
        static void Main()
        {
            char choice;
            Console.WriteLine("Enter Your Choice  ");
            choice = Convert.ToChar(Console.ReadLine());
            Switchcase obj = new Switchcase();
            obj.Check(choice);
        }
    }
}
