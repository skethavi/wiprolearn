using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiproDay5
{
   
        class Bank
        {
            public int AccountNo { get; } = 12;
            public string BranchName { get; } = "ECIL";
            public string BankName { get; } = "ICICI";
        }
        internal class ReadOnlyExample
        {
            static void Main()
            {
                Bank bank = new Bank();
                Console.WriteLine("Account No  " + bank.AccountNo);
                Console.WriteLine("Bank Name  " + bank.BankName);
                Console.WriteLine("Branch Name  " + bank.BranchName);
            }
        }
}
