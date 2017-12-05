using ByteBank.Portal.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure.Controllers
{
    public class ControllerFactory
    {
        private readonly IContainer _container;

        public ControllerFactory(IContainer container)
        {
            _container = container;
        }

        public ControllerBase GetController(string partialName)
        {
            var controllerClassName = $"{partialName}Controller";
            var controllerFullClassName = $"ByteBank.Portal.Controllers.{controllerClassName}";

            var controllerType = Type.GetType(controllerFullClassName);
            var controller = _container.Retrieve(controllerType);

            return (ControllerBase)controller;
        }
    }
}
