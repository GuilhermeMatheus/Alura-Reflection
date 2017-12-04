using ByteBank.Portal.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ByteBank.Portal.Infrastructure
{
    public class WebApplication
    {
        private readonly string[] _prefixes;

        public WebApplication(string[] prefixes)
        {
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            _prefixes = prefixes;
        }

        public void Start()
        {
            while (true)
            {
                var listener = new HttpListener();
                foreach (string s in _prefixes)
                    listener.Prefixes.Add(s);

                listener.Start();
                
                var context = listener.GetContext(); //blocking call
                var request = context.Request;
                var response = context.Response;

                var isFileRequest = IsFileRequest(request);
                if (isFileRequest)
                {
                    var handler = new FileRequestHandler();
                    handler.Handle(response, request.Url.LocalPath);
                }
                else if (request.Url.LocalPath.StartsWith("/Intercambio/USD"))
                {
                    var controller = new IntercambioController();
                    var page = controller.USD();
                    var handler = new PageFromStringRequestHandler(page);
                    handler.Handle(response, request.Url.LocalPath);
                }
                else if (request.Url.LocalPath.StartsWith("/Intercambio/MXN"))
                {
                    var controller = new IntercambioController();
                    var page = controller.MXN();
                    var handler = new PageFromStringRequestHandler(page);
                    handler.Handle(response, request.Url.LocalPath);
                }
                else if (request.Url.LocalPath.StartsWith("/Cartoes/Credito"))
                {
                    var controller = new CartoesController();
                    var page = controller.Credito();
                    var handler = new PageFromStringRequestHandler(page);
                    handler.Handle(response, request.Url.LocalPath);
                }
                else if (request.Url.LocalPath.StartsWith("/Cartoes/Debito"))
                {
                    var controller = new CartoesController();
                    var page = controller.Debito();
                    var handler = new PageFromStringRequestHandler(page);
                    handler.Handle(response, request.Url.LocalPath);
                }
                else
                {
                    var handler = new PageRequestHandler();
                    handler.Handle(response, request.Url.LocalPath);
                }

                response.OutputStream.Close();
                listener.Stop();
            }
        }

        private bool IsFileRequest(HttpListenerRequest request)
        {
            var urlPath = request.Url.LocalPath;
            var parts = urlPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var lastPart = parts.Last();

            return lastPart.Contains('.');
        }
    }
}
