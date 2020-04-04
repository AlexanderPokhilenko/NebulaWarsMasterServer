using System;
using Google.Apis.Upload;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchmakerTest
{
    [TestClass]
    public class NullableTypesTests
    {
        [TestMethod]
        public void Test()
        {
            //Arrange
            int? number =Init();
            //Act
            Console.WriteLine(number.Value);
            //Assert
        }

        private int? Init()
        {
            return null;
        }
    }
}