using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Services.Card
{
    public class CardService : ICardService
    {
        public string GetFeaturedCreditCard() =>
            "ByteBank Platinum Black";

        public string GetFeaturedDebitCard() =>
            "ByteBank Estudante";
    }
}
