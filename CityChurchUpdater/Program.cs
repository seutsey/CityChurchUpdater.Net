using System;
using System.IO;
using System.Net;
using DropNet;

namespace CityChurchUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            string _app_key = "z5f3fr8f58da068";
            string _app_secret = "dae462er8v1bux5";
            string _api_token = "GVy7-5wFLiEAAAAAAAAEQvLueCHTfltqlw8K_6uiNnkYACq97uvmJilwJk1_rl9C";

            DropNetClient _client = new DropNetClient(_app_key, _app_secret, _api_token);  
            var metadata = _client.GetMetaData("/website");

            string _db_path = "";
            DateTime d = new DateTime(2000, 1, 1);
            foreach (var x in metadata.Contents)
            {
                if (DateTime.Parse(x.Modified) > d && x.Extension == ".jpg")
                {
                    d = DateTime.Parse(x.Modified);
                    _db_path = x.Path;
                }
            }
       
            var _fileBytes = _client.GetFile(_db_path);

            FtpWebRequest _request = (FtpWebRequest)WebRequest.Create("ftp://citychurch.us/wp-content/uploads/whats_happening.jpg");
            _request.Method = WebRequestMethods.Ftp.UploadFile;

            _request.Credentials = new NetworkCredential("seutsey", "myQyP&zJPE8d");

            _request.ContentLength = _fileBytes.Length;
            Stream _request_stream = _request.GetRequestStream();
            
            _request_stream.Write(_fileBytes, 0, _fileBytes.Length);
            _request_stream.Close();
        }
    }
}
