using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopsExample
{
    internal class CricketEx
    {
        static int score;
        public void Increment(int x)
        {
            score+=x;
        }
        static void Main()
        {
            CricketEx cricket1 = new CricketEx();
            CricketEx cricket2 = new CricketEx();
            CricketEx cricket3 = new CricketEx();
            cricket1.Increment(1);
            cricket2.Increment(2);
            cricket3.Increment(3);
            Console.WriteLine("score"+ CricketEx.score);
        }
    }
}
