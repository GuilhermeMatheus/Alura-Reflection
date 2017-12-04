using ByteBank.Portal.Infrastructure;
using ByteBank.Services;
using ByteBank.Services.Card;
using ByteBank.Services.Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Controllers
{
    public class CartoesController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CartoesController()
        {
            _cardService = new CardService();
        }

        public string Credito()
        {
            var view = GetFileContent("ByteBank.Portal.View.Cartoes.Credito.html");
            var creditCard = _cardService.GetFeaturedCreditCard();

            view = view.Replace("CARTAO_EM_DESTAQUE", creditCard);
            return view;
        }

        public string Debito()
        {
            var view = GetFileContent("ByteBank.Portal.View.Cartoes.Debito.html");
            var debitCard = _cardService.GetFeaturedDebitCard();

            view = view.Replace("CARTAO_EM_DESTAQUE", debitCard);
            return view;
        }
    }
}
