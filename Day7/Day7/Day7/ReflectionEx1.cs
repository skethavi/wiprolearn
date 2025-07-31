﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    internal class ReflectionEx1
    {
        static void Main()
        {
            Type typeObj = typeof(Test);
            Console.WriteLine("Methods Avaialble in Test Class Are");
            foreach (MethodInfo mi in typeObj.GetMethods())
            {
                Console.WriteLine(mi.Name);
            }
            Console.WriteLine("Variables available in the class are ");
            foreach (FieldInfo fi in typeObj.GetFields())
            {
                Console.WriteLine(fi.Name);
            }
        }
    }
}
