﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceEx1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITraining[] arr = new ITraining[]
            {
                new Sunil(), new Nandini()
            };

            foreach (ITraining i in arr)
            {
                i.Name();
                i.Email();
                Console.WriteLine("---------------------------");
            }
        }
    }
}
