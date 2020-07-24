using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Services.GoogleApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkLibrary.Http.Utils;
using Newtonsoft.Json;

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
        
        [TestMethod]
        public void Test51()
        {
            byte[] arr1 = new byte[] {12, 198, 16, 168, 54};
            byte[] arr2 = null;
            if (arr1.SequenceEqual(arr2))
            {
                Console.WriteLine("equal");
            }
            else
            {
                Console.WriteLine("not equal");
            }
        }
        
        [TestMethod]
        public void Test3()
        {
            string json = @" {
  ""purchaseTimeMillis"": ""1595000674505"",
  ""purchaseState"": 0,
  ""consumptionState"": 0,
  ""developerPayload"": """",
  ""orderId"": ""GPA.3347-1869-0815-86288"",
  ""purchaseType"": 0,
  ""acknowledgementState"": 0,
  ""kind"": ""androidpublisher#productPurchase"",
  ""obfuscatedExternalAccountId"": ""test""
}
";
            GoogleResponse googleResponse = JsonConvert.DeserializeObject<GoogleResponse>(json);
            Assert.AreEqual("test", googleResponse.ObfuscatedExternalAccountId);
        }
        
         
        [TestMethod]
        public void Test4()
        {
            string original = "ajisfbvj2974=21ok3,z1-o2,.cmefj=2c084f";
            string copy = original.Caesar(10).Caesar(-10);
            Assert.AreEqual(original, copy);
        }
    }
}