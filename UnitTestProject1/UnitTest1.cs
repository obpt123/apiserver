using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var pinfo = typeof(T).GetProperty("Name");
        }

        public class T : IName
        {
            public string Name { get; set; }
        }
        public interface IName
        {
             string Name { get; set; }
        }
    }
}
