using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Services.GoogleApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkLibrary.Http.Utils;
using NetworkLibrary.NetworkLibrary.Http;
using Newtonsoft.Json;
using ZeroFormatter;

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
        public void Test1232()
        {
            ProductModel productModel = new ProductModel()
            {
                Id = 54,
                CostModel = new CostModel()
                {
                    CostTypeEnum = CostTypeEnum.HardCurrency,
                    SerializedCostModel = ZeroFormatterSerializer.Serialize(new InGameCurrencyCostModel()
                    {
                        Cost = 45
                    })
                },
                IsDisabled = false,
                PreviewImagePath = "suka",
                SerializedModel = ZeroFormatterSerializer.Serialize(new SoftCurrencyProductModel()
                {
                    Amount = 156
                }),
                ResourceTypeEnum = ResourceTypeEnum.SoftCurrency,
                ProductSizeEnum = ProductSizeEnum.Small,
                ProductMark = null
            };

            var arr1 = ZeroFormatterSerializer.Serialize(productModel);
            var base64String = Convert.ToBase64String(arr1);
            var arr2 = Convert.FromBase64String(base64String);
            
            Assert.AreEqual(arr1.Length, arr2.Length);
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
         
        [TestMethod]
        public void Test5()
        {
            WarshipPowerPointsResourceModel wpp = new WarshipPowerPointsResourceModel()
            {
                
            };
            string skinName = wpp.WarshipTypeEnum.ToString();
            Console.WriteLine(skinName);
        }  
        
        [TestMethod]
        public void Test29()
        {
            WarshipPowerPointsProductModel model = new WarshipPowerPointsProductModel()
            {
                Increment = 16,
                WarshipId = 4,
                WarshipTypeEnum = WarshipTypeEnum.Sage,
                SupportClientModel = null,
            };

            byte[] data = ZeroFormatterSerializer.Serialize(model);
            var restored = ZeroFormatterSerializer
                .Deserialize<WarshipPowerPointsProductModel>(data);

            restored.SupportClientModel = new WppSupportClientModel()
            {
                StartValue = 654,
                WarshipSkinName = "advjb",
                MaxValueForLevel = 92992
            };

            var data2 = ZeroFormatterSerializer.Serialize(restored);
            var restored2 = ZeroFormatterSerializer.Deserialize<WarshipPowerPointsProductModel>(data2);
            int i = 9;
        }
    }
}