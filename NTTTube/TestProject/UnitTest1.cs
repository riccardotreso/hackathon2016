using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NTTTube.DB;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestUser()
        {
            var listuser = Repository.GetUser("tresor");
            Assert.AreNotEqual(listuser, null);
        }
    }
}
