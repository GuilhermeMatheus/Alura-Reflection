using ByteBank.Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ByteBank.Portal
{
    class Program
    {
        private static string[] prefixes = new[] { "http://bytebank.com:55345/" };

        static void Main(string[] args)
        {
            var webApp = new WebApplication(prefixes);
            webApp.Start();
        }
    }
}
