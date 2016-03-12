using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NTTTube.FTP
{
    public class Helper
    {
        private static string ftpurl = ConfigurationManager.AppSettings["FtpUrl"].ToString();
        private static string username = ConfigurationManager.AppSettings["FtpUser"].ToString();
        private static string password = ConfigurationManager.AppSettings["FtpPassword"].ToString();


        //public static async Task Upload(byte[] fileContents, string fileName)
        public static void Upload(byte[] fileContents, string fileName)
        {

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpurl + fileName);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            
            request.Credentials = new NetworkCredential(username, password);
            
            request.ContentLength = fileContents.Length;

            request.UsePassive = false;
            request.UseBinary = true;
            request.KeepAlive = true;


            //Stream requestStream = await request.GetRequestStreamAsync();
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            response.Close();

        }
    }
}
