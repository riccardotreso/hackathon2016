using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTTTube.DB;
using System.Collections.Generic;
using System.Linq;
using NTTTube.Model;

namespace TestProject
{
    [TestClass]
    public class Video
    {
        [TestMethod]
        public void InsertChannel()
        {
            /*
            Repository.InsertChannel("Comunicazione");
            Repository.InsertChannel("Formazione");
            Repository.InsertChannel("Idee");
            var id = Repository.InsertChannel("Presentazioni");

            Assert.AreNotEqual(id, null);
            */
        }

        [TestMethod]
        public void GetChannels()
        {

            var list = Repository.GetAllChannel();
            Assert.AreNotEqual(list.Count(), 0);
        }

        [TestMethod]
        public void InsertVideo()
        {

            var id = Repository.InsertVideo(new NTTTube.Model.Video() {
                path = "http://ntt-mediaservice.cloudapp.net/video/",
                description = "test video streaming",
                username = "tresor",
                title ="Adele",
                category = "Private",
                channel = "Aziendale",
                date = DateTime.Now
            });

            var result = Repository.InsertComment(id, new NTTTube.Model.Comment()
            {
                data = DateTime.Now,
                text = "questo video mi piace",
                username = "tresor"

            });

            result = Repository.InsertLike(id, "christian");
            result = Repository.InsertUnlike(id, "alessandro");

            Assert.AreEqual(result, true);


           
        }

        [TestMethod]
        public void GetVideo()
        {

            var result = Repository.GetVideo("56e3e62d34393d184cc2cd55");
            Assert.AreNotEqual(result, null);
        }

        
    }
}
