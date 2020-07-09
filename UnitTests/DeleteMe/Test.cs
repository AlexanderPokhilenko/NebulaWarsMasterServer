using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchmakerTest.DeleteMe
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            string millis = "1591866516468";
            long.TryParse(millis, out long unixTime);
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTime);
            Console.WriteLine(dateTimeOffset.DateTime);
        }
        
        [TestMethod]
        public void Test2()
        {
           List<int> test = new List<int>()
           {
               65,19,861,654
           };
           var test1  = test.Take(10).ToList();
        }
    }
}