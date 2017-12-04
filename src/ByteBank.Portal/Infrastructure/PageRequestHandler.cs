using ByteBank.Portal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure
{
    public class PageRequestHandler
    {
        public void Handle(HttpListenerResponse response, string localPath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var pageName = FileUtils.ResolveResourceFullQualifiedName($"/View{localPath}") + ".html";
            var resourceStream = assembly.GetManifestResourceStream(pageName);

            if (resourceStream == null)
                response.StatusCode = 404;
            else
                using (resourceStream)
                {
                    response.ContentType = "text/html; charset=utf-8";
                    byte[] fileBuffer = new byte[resourceStream.Length];
                    resourceStream.Read(fileBuffer, 0, fileBuffer.Length);

                    response.ContentLength64 = fileBuffer.Length;
                    var output = response.OutputStream;
                    output.Write(fileBuffer, 0, fileBuffer.Length);
                }
        }
    }
}
