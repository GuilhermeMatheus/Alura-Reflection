using ByteBank.Portal.Infrastructure.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure.Filters
{
    public class FilterResolver
    {
        public ActionFilterResult ExecuteFilters(ControllerBase controller, ActionBindInfo actionBindInfo)
        {
            var controllerType = controller.GetType();

            var controllerActionFilters = controllerType.GetCustomAttributes(typeof(ActionFilterAttribute), false);
            var actionFilters = actionBindInfo.MethodInfo.GetCustomAttributes(typeof(ActionFilterAttribute), false);

            var union = controllerActionFilters.Union(actionFilters).Cast<ActionFilterAttribute>();

            foreach (var item in union)
            {
                if(!item.CanContinue())
                    return ActionFilterResult.Redirect(item.RedirectTo);
            }

            return ActionFilterResult.Continue();
        }
    }
}
