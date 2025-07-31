using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;


namespace MockEx2
{
    public interface IEmployDao
    {
        List<Employ> ShowEmploy();
        Employ SearchEmploy(int empno);
    }
}
