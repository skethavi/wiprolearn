using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Program to separate string after space and add to array
/// </summary>
namespace WiproDay2
{
    internal class Milestone2
    {
        static void Main()
        {
            string s = "Welcome to dotnet programming ";
            string[] names = s.Split(' ');
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
