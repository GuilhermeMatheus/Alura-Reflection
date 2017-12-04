using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure
{
    public class PageFromStringRequestHandler
    {
        private readonly string _htmlResponse;

        public PageFromStringRequestHandler(string htmlResponse)
        {
            _htmlResponse = htmlResponse;
        }

        public void Handle(HttpListenerResponse response, string localPath)
        {
            response.ContentType = "text/html; charset=utf-8";
            var fileBuffer = Encoding.UTF8.GetBytes(_htmlResponse);

            response.ContentLength64 = fileBuffer.Length;
            var output = response.OutputStream;
            output.Write(fileBuffer, 0, fileBuffer.Length);
        }
    }
}
