using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay2
{
    internal class Arrcopy
    {
        public void Show()
        {
            int[] a = new int[] { 12, 13, 34, 56 };
            int[] b = a;
            for(int i = 0; i < b.Length; i++)
            {
                Console.WriteLine(b[i]);
            }
        }
        static void Main(string[] args)
        {
            Arrcopy obj= new Arrcopy();
            obj.Show();
        }
        

    }
}
