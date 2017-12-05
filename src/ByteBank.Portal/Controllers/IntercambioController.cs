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
            var exchangeValue = _exchangeService.Calc(1, "MXN", "BRL");
            return View(new { ValorEmReal = exchangeValue });
        }

        public string USD()
        {
            var exchangeValue = _exchangeService.Calc(1, "USD", "BRL");
            return View(new { ValorEmReal = exchangeValue });
        }

        public string Calculo(string from, string to, decimal value)
        {
            var exchangeValue = _exchangeService.Calc(value, from, to);
            return View(new {
                Valor = value,
                MoedaOrigem = from,
                ValorEmReal = exchangeValue
            });
        }
    }
}
