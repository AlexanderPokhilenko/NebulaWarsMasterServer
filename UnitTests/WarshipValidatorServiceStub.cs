using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

[TestClass]
public class TestDich
{
    [TestMethod]
    public void Test1()
    {
        string developerPayload = "bXkgZGljaCBwYXlsb2FkIGcxNzgyMjc4NDQ0OTA1MjY2MDYyNg";
        JObject jObject = new JObject();
        jObject.Add("developerPayload", developerPayload);
        string developerPayloadJson = jObject.ToString();
        Console.WriteLine(developerPayloadJson);
    }
}