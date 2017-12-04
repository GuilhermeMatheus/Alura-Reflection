using ByteBank.Portal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure
{
    public class ControllerBase
    {
        protected string GetFileContent(string fullQualifiedName)
        {
            var result = FileUtils.GetEmbeddedResourceContent(fullQualifiedName);
            return result;
        }
    }
}
