﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionEx1
{
    internal static class Extension
    {
        public static int Mult(this Calculation calc, int a, int b)
        {
            return a * b;
        }

    }
}
