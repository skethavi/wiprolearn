using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay2
{
    internal class BoxEmploy
    {
        public void Show(object obj)
        {
            string type = obj.GetType().Name;
            if (type.Equals("Employ"))
            {
                Employ employ = (Employ)obj;
                Console.WriteLine("Employ No  " + employ.empno + " Name " + employ.name + " Basic " + employ.basic);
            }
        }
        static void Main()
        {
            Employ employ = new Employ();
            employ.empno = 21;
            employ.name = "Kethavi";
            employ.basic = 56990;

            BoxEmploy boxEmploy = new BoxEmploy();
            boxEmploy.Show(employ);
        }
    }
}
