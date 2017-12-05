using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure.Binding
{
    public class ActionBinder
    {
        public ActionBindInfo GetActionBindInfo(string actionPart, object controller)
        {
            var questionMarkIdx = actionPart.IndexOf('?');
            var actionHasArguments = questionMarkIdx >= 0;

            if (actionHasArguments)
            {
                var arguments = actionPart.Substring(questionMarkIdx + 1);
                var actionName = actionPart.Substring(0, questionMarkIdx);
                var argumentsAndValues = GetArgumentsAndValues(arguments);
                var argumentsNames = argumentsAndValues.Select(pair => pair.Name).ToArray();

                var methodInfo = FindMethodByNameAndArgumentsNames(actionName, argumentsNames, controller);
                return new ActionBindInfo(methodInfo, argumentsAndValues);
            }
            else
            {
                var methodInfo = controller.GetType().GetMethod(actionPart);
                return new ActionBindInfo(methodInfo, Enumerable.Empty<ArgumentPair>());
            }
        }
        
        private IEnumerable<ArgumentPair> GetArgumentsAndValues(string arguments)
        {
            //Isso podemos explicar com o auxilio do LinqPad
            var argumentAndValuePairs = arguments.Split('&');

            foreach (var item in argumentAndValuePairs)
            {
                var pair = item.Split('=');
                yield return new ArgumentPair(pair[0], pair[1]);
            }
        }

        private MethodInfo FindMethodByNameAndArgumentsNames(string name, string[] arguments, object obj)
        {
            var methods = obj.GetType().GetMethods();
            var overloads = methods.Where(method => method.Name == name);

            foreach (var item in overloads)
            {
                var parameters = item.GetParameters();
                if (parameters.Length != arguments.Length)
                    continue;

                var match = parameters.All(
                    parameter =>
                        arguments.Any(arg => arg.Equals(parameter.Name, StringComparison.OrdinalIgnoreCase))
                );

                if (match)
                    return item;
            }

            throw new ArgumentOutOfRangeException($"Sobrecarga de método '{name}' não encontrada");
        }
    }
}
