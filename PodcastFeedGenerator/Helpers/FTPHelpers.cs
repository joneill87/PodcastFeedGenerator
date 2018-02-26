using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace PodcastFeedGenerator.Helpers
{
    public class FTPClient
    {
        private Uri webAccessibleURI = new Uri("http://cursedshores.codeprimer.net/files/");
        private string ftpHost = "ftp.codeprimer.net";
        private string username = "fergus@cursedshores.codeprimer.net";
        private string password = "[nsc)Tv{.w@O";

        private FtpClient GetFtpClient()
        {
            FtpClient ftpClient = new FtpClient(ftpHost);
            ftpClient.Credentials = new NetworkCredential(username, password);
            return ftpClient;
        }

        public string UploadFile(HttpPostedFileBase fileBase)
        {
           
            string filePath = "/files/" + fileBase.FileName;

            var ftpClient = GetFtpClient();
            ftpClient.Connect();

            ftpClient.Upload(fileBase.InputStream, fileBase.FileName);
            return new Uri(webAccessibleURI, filePath).ToString();
        }

        public string UploadFeed(string feedContents)
        {
            string filePath = "/files/feed.xml";

            var ftpClient = GetFtpClient();
            ftpClient.Connect();

            var fileBytes = System.Text.Encoding.UTF8.GetBytes(feedContents);
            ftpClient.Upload(fileBytes, "feed.xml");
            return new Uri(webAccessibleURI, filePath).ToString();
        }
    }
}