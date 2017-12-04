using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Utils
{
    public class FileUtils
    {
        public static string ResolveContentType(string path)
        {
            var isJS = path.EndsWith(".js", StringComparison.OrdinalIgnoreCase);
            if (isJS)
                return "application/javascript; charset=utf-8";

            var isCSS = path.EndsWith(".css", StringComparison.OrdinalIgnoreCase);
            if (isCSS)
                return "text/css; charset=utf-8";

            throw new NotImplementedException();
        }

        public static string ResolveResourceFullQualifiedName(string path)
        {
            var resourceNamespace = "ByteBank.Portal";
            var resourcePath = path.Replace('/', '.');
            return $"{resourceNamespace}{resourcePath}";
        }

        public static string GetEmbeddedResourceContent(string fullQualifiedName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream(fullQualifiedName);

            var reader = new StreamReader(resourceStream);
            var result = reader.ReadToEnd();

            return result;
        }
    }
}
