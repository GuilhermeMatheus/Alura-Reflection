using ByteBank.Portal.Infrastructure.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure
{
    public class ControllerRequestHandler
    {
        private readonly ActionBinder _actionBinder = new ActionBinder();

        public void Handle(HttpListenerResponse response, string localPath)
        {
            var portions = localPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            var controllerClassName = $"{portions[0]}Controller";
            var controllerFullClassName = $"ByteBank.Portal.Controllers.{controllerClassName}";

            // Explicar o que é um ObjectHandle
            var controller = Activator.CreateInstance("ByteBank.Portal", controllerFullClassName);
            var realController = controller.Unwrap();

            var actionName = portions[1];
            var actionBindInfo = _actionBinder.GetActionBindInfo(actionName, realController);

            var actionResult = actionBindInfo.Invoke<string>(realController);

            response.ContentType = "text/html; charset=utf-8";
            var fileBuffer = Encoding.UTF8.GetBytes(actionResult);

            response.ContentLength64 = fileBuffer.Length;
            var output = response.OutputStream;
            output.Write(fileBuffer, 0, fileBuffer.Length);
        }
    }
}
