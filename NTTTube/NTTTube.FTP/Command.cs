using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NTTTube.FTP
{
    public class Command
    {
        public static void SendFtpCommand(string ftpServer, string filePath, string pUserName, string pPassword)
        {
            var serverName = ftpServer;
            var port = 21;
            var userName = pUserName;
            var password = pPassword;
            var command = "SITE CHMOD 777 " + filePath;

            var tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(serverName, port);
                Flush(tcpClient);

                var response = TransmitCommand(tcpClient, "user " + userName);
                if (response.IndexOf("331", StringComparison.OrdinalIgnoreCase) < 0)
                    throw new Exception(string.Format("Error \"{0}\" while sending user name \"{1}\".", response, userName));

                response = TransmitCommand(tcpClient, "pass " + password);
                if (response.IndexOf("230", StringComparison.OrdinalIgnoreCase) < 0)
                    throw new Exception(string.Format("Error \"{0}\" while sending password.", response));

                response = TransmitCommand(tcpClient, command);
                if (response.IndexOf("200", StringComparison.OrdinalIgnoreCase) < 0)
                    throw new Exception(string.Format("Error \"{0}\" while sending command \"{1}\".", response, command));
            }
            finally
            {
                if (tcpClient.Connected)
                    tcpClient.Close();
            }
        }

        private static string TransmitCommand(TcpClient tcpClient, string cmd)
        {
            var networkStream = tcpClient.GetStream();
            if (!networkStream.CanWrite || !networkStream.CanRead)
                return string.Empty;

            var sendBytes = Encoding.ASCII.GetBytes(cmd + "\r\n");
            networkStream.Write(sendBytes, 0, sendBytes.Length);

            var streamReader = new StreamReader(networkStream);
            return streamReader.ReadLine();
        }

        private static string Flush(TcpClient tcpClient)
        {
            try
            {
                var networkStream = tcpClient.GetStream();
                if (!networkStream.CanWrite || !networkStream.CanRead)
                    return string.Empty;

                var receiveBytes = new byte[tcpClient.ReceiveBufferSize];
                networkStream.ReadTimeout = 10000;
                networkStream.Read(receiveBytes, 0, tcpClient.ReceiveBufferSize);

                return Encoding.ASCII.GetString(receiveBytes);
            }
            catch
            {
                // Ignore all irrelevant exceptions
            }

            return string.Empty;
        }
    }
}
