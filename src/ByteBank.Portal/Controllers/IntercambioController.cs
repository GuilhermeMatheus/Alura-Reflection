using ByteBank.Portal.Infrastructure;
using ByteBank.Services;
using ByteBank.Services.Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Controllers
{
    public class IntercambioController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;

        public IntercambioController()
        {
            _exchangeService = new RealTimeExchangeService();
        }

        public string MXN()
        {
            var view = GetFileContent("ByteBank.Portal.View.Intercambio.MXN.html");
            var exchangeValue = _exchangeService.Calc(1, "MXN", "BRL");
            view = view.Replace("VALOR_EM_REAL", exchangeValue.ToString());
            return view;
        }

        public string USD()
        {
            var view = GetFileContent("ByteBank.Portal.View.Intercambio.USD.html");
            var exchangeValue = _exchangeService.Calc(1, "USD", "BRL");
            view = view.Replace("VALOR_EM_REAL", exchangeValue.ToString());
            return view;
        }
    }
}
