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
    public class FileRequestHandler
    {
        public void Handle(HttpListenerResponse response, string localPath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = FileUtils.ResolveResourceFullQualifiedName(localPath);
            var resourceStream = assembly.GetManifestResourceStream(resourceName);

            if (resourceStream == null)
                response.StatusCode = 404;
            else
                using (resourceStream)
                {
                    response.ContentType = FileUtils.ResolveContentType(localPath);
                    byte[] fileBuffer = new byte[resourceStream.Length];
                    resourceStream.Read(fileBuffer, 0, fileBuffer.Length);

                    response.ContentLength64 = fileBuffer.Length;
                    var output = response.OutputStream;
                    output.Write(fileBuffer, 0, fileBuffer.Length);
                }
        }

        
    }
}
