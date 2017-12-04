using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
