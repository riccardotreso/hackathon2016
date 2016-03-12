using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class FTP
    {
        [TestMethod]
        public void Upload()
        {
           var byteArr = File.ReadAllBytes("C:\\Users\\tresor\\Downloads\\Francesca_Michielin_-_Lamore_esiste.mp4");
           NTTTube.FTP.Helper.Upload(byteArr, Guid.NewGuid().ToString() + ".mp4");



        }


        [TestMethod]
        public void CommandFTP()
        {
            NTTTube.FTP.Command.SendFtpCommand("ntt-mediaservice.cloudapp.net", "/video/6311b1cb-6de7-4656-b57a-269c0b35584e.mp4", "myuser", "mypass");
        }
    }
}
