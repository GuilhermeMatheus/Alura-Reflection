using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Services.Exchange
{
    public class RealTimeExchangeService : IExchangeService
    {
        private static Random _rdm = new Random();

        public decimal Calc(decimal value, string from, string to) =>
            value* (decimal) _rdm.NextDouble();
    }
}
