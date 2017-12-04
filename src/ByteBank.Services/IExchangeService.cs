using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Services
{
    public interface IExchangeService
    {
        decimal Calc(decimal value, string from, string to);
    }
}
