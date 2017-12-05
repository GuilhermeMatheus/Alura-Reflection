using ByteBank.Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Controllers
{
    public class ErrorController : ControllerBase
    {
        public string Index() =>
            View();
    }
}
