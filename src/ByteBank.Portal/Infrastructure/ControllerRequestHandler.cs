using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure
{
    public class ControllerRequestHandler
    {
        public void Handle(HttpListenerResponse response, string localPath)
        {
            var portions = localPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            var controllerClassName = $"{portions[0]}Controller";
            var actionName = portions[1];

            var controllerFullClassName = $"ByteBank.Portal.Controllers.{controllerClassName}";

            // Explicar o que é um ObjectHandle
            var controller = Activator.CreateInstance("ByteBank.Portal", controllerFullClassName);
            var realController = controller.Unwrap();
            var actionMethodInfo = realController.GetType().GetMethod(actionName);

            var actionResult = (string)actionMethodInfo.Invoke(realController, new object[0]);

            response.ContentType = "text/html; charset=utf-8";
            var fileBuffer = Encoding.UTF8.GetBytes(actionResult);

            response.ContentLength64 = fileBuffer.Length;
            var output = response.OutputStream;
            output.Write(fileBuffer, 0, fileBuffer.Length);
        }
    }
}
