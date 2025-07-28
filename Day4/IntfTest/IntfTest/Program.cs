using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace IntfTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IOne obj1 = new Test();
            obj1.Show();

            ITwo obj2 = new Test();
            obj2.Show();
        }
    }
}
