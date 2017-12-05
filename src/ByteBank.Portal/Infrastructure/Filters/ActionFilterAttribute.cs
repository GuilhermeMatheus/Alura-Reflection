using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure.Filters
{
    public abstract class ActionFilterAttribute : Attribute
    {
        public string RedirectTo { get; protected set; }
        public abstract bool CanContinue();
    }
}
