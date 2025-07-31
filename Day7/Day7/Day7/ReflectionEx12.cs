using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    internal class ReflectionEx12
    {
        static void Main()
        {
            Type objStudent = typeof(StudentR);
            Console.WriteLine("Methods available are ");
            foreach (MethodInfo m in objStudent.GetMethods())
            {
                Console.WriteLine(m.Name);
            }
            Console.WriteLine("Fields available are  ");
            foreach (FieldInfo f in objStudent.GetFields())
            {
                Console.WriteLine(f.Name);
            }
        }
    }
}
