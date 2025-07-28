using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace FileHandling
{
    internal class ReadEmployFile
    {
        static void Main()
        {
            FileStream fs=new FileStream(@"C:\Users\ketha\OneDrive\Documents\Fileex.txt", FileMode.Open, FileAccess.Read);
            BinaryFormatter binaryFormatter=new BinaryFormatter();
            Employ employ=(Employ)binaryFormatter.Deserialize(fs);
            Console.WriteLine(employ);
            

        }
    }
}

    