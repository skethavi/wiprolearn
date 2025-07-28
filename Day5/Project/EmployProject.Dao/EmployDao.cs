using EmployProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace EmployProject.Dao
{
    internal interface EmployDao
    {
        string AddEmployDao(Employ employ);
        List<Employ> ShowEmployDao();
        Employ SearchEmployDao(int empno);
    }
}
