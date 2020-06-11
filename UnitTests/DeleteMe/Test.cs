using System;
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
    }
}