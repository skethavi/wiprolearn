using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay2
{
    internal class Arraystr
    {
        public void Show()
        {
            string[] str = new string[] { "Rajesh", "Dilip", "Basha", "Sreeja", "Anusha" };
            foreach (string strr in str)
            {
                Console.WriteLine(strr);
               
            }
        }
        static void Main()
        {
            Arraystr obj= new Arraystr();
            obj.Show();
        }
    }
}
