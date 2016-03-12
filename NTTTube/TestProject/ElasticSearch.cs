using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class ElasticSearch
    {
        [TestMethod]
        public void search()
        {
            var elasticHelper = new NTTTube.Elastic.Helper();
            elasticHelper.Connect();
            elasticHelper.Search("stronzo");
        }
    }
}
