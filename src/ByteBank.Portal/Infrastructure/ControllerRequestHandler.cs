using ByteBank.Portal.Infrastructure.Binding;
using ByteBank.Portal.Infrastructure.Controllers;
using ByteBank.Portal.Infrastructure.Filters;
using ByteBank.Portal.Infrastructure.IoC;
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
        private readonly ControllerFactory _controllerFactory;
        private readonly FilterResolver _filterResolver = new FilterResolver();

        public ControllerRequestHandler(IContainer container)
        {
            _controllerFactory = new ControllerFactory(container);
        }

        public void Handle(HttpListenerResponse response, string localPath)
        {
            var portions = localPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            var realController = _controllerFactory.GetController(portions[0]);

            var actionName = portions[1];
            var actionBindInfo = _actionBinder.GetActionBindInfo(actionName, realController);

            var filterResult = _filterResolver.ExecuteFilters(realController, actionBindInfo);

            if (filterResult.CanContinue)
            {
                var actionResult = actionBindInfo.Invoke<string>(realController);

                response.ContentType = "text/html; charset=utf-8";
                var fileBuffer = Encoding.UTF8.GetBytes(actionResult);

                response.ContentLength64 = fileBuffer.Length;
                var output = response.OutputStream;
                output.Write(fileBuffer, 0, fileBuffer.Length);
            }
            else
            {
                response.Redirect(filterResult.RedirectTo);
            }
        }
    }
}
