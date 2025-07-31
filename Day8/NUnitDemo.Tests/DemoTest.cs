using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnitDemos;


namespace NUnitDemo.Tests
{
    [TestFixture]
    public class DemoTest
    {
        [Test]
        public void TestSum()
        {
            Demo demo = new Demo();
            Assert.AreEqual(5, demo.Sum(2, 3));
        }
        [Test]
        public void TestSayHello()
        {
            Demo demo = new Demo();
            Assert.AreEqual("Welcome to C# FSD Programming...", demo.SayHello());
        }
    }
}
