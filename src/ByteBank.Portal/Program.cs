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
    public abstract class Dad
    {
        public void M()
        {
            "".ToString();
        }
    }

    public class Child : Dad
    {
        public Child()
        {
            M();
        }
    }

    class Program
    {
        private static string[] prefixes = new[] { "http://bytebank.com:55345/" };

        static void Main(string[] args)
        {
            new Child();

            var webApp = new WebApplication(prefixes);
            webApp.Start();
        }
    }
}
