using ByteBank.Portal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure
{
    public class ControllerBase
    {
        protected string View([CallerMemberName]string viewName = null)
        {
            if (String.IsNullOrEmpty(viewName))
                throw new ArgumentNullException(nameof(viewName));

            var directoryName = GetType().Name.Replace("Controller", "");

            var fullQualifiedName = $"ByteBank.Portal.View.{directoryName}.{viewName}.html";

            return GetFileContent(fullQualifiedName);
        }

        protected string View(object model, [CallerMemberName]string viewName = null)
        {
            var allModelProperties = model.GetType().GetProperties();
            var rawView = View(viewName);

            var regex = new Regex("\\{{(.*?)\\}}");
            var matches = regex.Matches(rawView);

            var bindedView = regex.Replace(rawView, (match) => {
                var propertyName = match.Groups[1].Value;
                var propertyInfo = allModelProperties.First(_ => _.Name == propertyName);

                var value = propertyInfo.GetValue(model);

                return value?.ToString();
            });

            return bindedView;
        }

        protected string GetFileContent(string fullQualifiedName)
        {
            var result = FileUtils.GetEmbeddedResourceContent(fullQualifiedName);
            return result;
        }
    }
}
