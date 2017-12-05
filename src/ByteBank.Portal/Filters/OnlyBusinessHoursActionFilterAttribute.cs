using ByteBank.Portal.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OnlyBusinessHoursActionFilterAttribute : ActionFilterAttribute
    {
        public OnlyBusinessHoursActionFilterAttribute() : this("/Error/Index") { }

        public OnlyBusinessHoursActionFilterAttribute(string redirectPath)
        {
            RedirectTo = redirectPath;
        }

        public override bool CanContinue()
        {
            var now = DateTime.Now;

            if (now.DayOfWeek == DayOfWeek.Saturday)
                return false;

            if (now.DayOfWeek == DayOfWeek.Sunday)
                return false;

            if (now.Hour <= 8 || now.Hour >= 16)
                return false;

            return true;
        }
    }
}
