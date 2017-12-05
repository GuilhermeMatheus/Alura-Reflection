using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure.Filters
{
    public class ActionFilterResult
    {
        public bool CanContinue { get; private set; }
        public string RedirectTo { get; private set; }

        private ActionFilterResult() { }

        public static ActionFilterResult Continue() =>
            new ActionFilterResult { CanContinue = true };

        public static ActionFilterResult Redirect(string to) =>
            new ActionFilterResult { CanContinue = false, RedirectTo = to };
    }
}
