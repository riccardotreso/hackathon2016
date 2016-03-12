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
        private static string ftpServer = ConfigurationManager.AppSettings["FtpServer"].ToString();
        private static string ftpFolder = ConfigurationManager.AppSettings["FtpFolder"].ToString();
        private static string username = ConfigurationManager.AppSettings["FtpUser"].ToString();
        private static string password = ConfigurationManager.AppSettings["FtpPassword"].ToString();


        //public static async Task Upload(byte[] fileContents, string fileName)
        public static void Upload(byte[] fileContents, string fileName)
        {

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(string.Format("ftp://{0}{1}{2}", ftpServer, ftpFolder, fileName));
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

            //Change Permission
            NTTTube.FTP.Command.SendFtpCommand(ftpServer, string.Format("{0}{1}", ftpFolder, fileName), username, password);

        }
    }
}
