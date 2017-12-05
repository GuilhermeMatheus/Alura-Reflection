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

        public CartoesController(ICardService cardService)
        {
            _cardService = cardService;
        }

        public string Credito()
        {
            var creditCard = _cardService.GetFeaturedCreditCard();
            // Primeiro usamos a abordagem
            //    public class CartaoModel { public string CartaoDestaque { get; set; } } 
            //    var model = new CartaoModel();
            //    model.CartaoDestaque = creditCard;
            //
            // Em seguida
            //    dynamic model; 
            //    model.CartaoDestaque = creditCard; 
            //
            // Enfim
            //    new { CartaoDestaque = creditCard }; <- oportunidade de dizer dynamicModel.method()
            var model = new { CartaoDestaque = creditCard };

            return View(model);
        }

        public string Debito()
        {
            var debitCard = _cardService.GetFeaturedDebitCard();
            var model = new { CartaoDestaque = debitCard };

            return View(model);
        }
    }
}
