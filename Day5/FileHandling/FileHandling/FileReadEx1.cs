using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandling
{
    internal class FileReadEx1
    {
        static void Main()
        {
            FileStream fs =
                new FileStream(@"D:\wipro\Day1\HelloWorld\HelloWorld\Stringex.cs", FileMode.Open,
                FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            sr.Close();
            fs.Close();
        }
    }
}
