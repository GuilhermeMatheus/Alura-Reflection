using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure.Binding
{
    public class ActionBindInfo
    {
        public MethodInfo MethodInfo { get; private set; }
        public IReadOnlyCollection<ArgumentPair> Arguments { get; private set; }

        public ActionBindInfo(MethodInfo methodInfo, IEnumerable<ArgumentPair> arguments)
        {
            MethodInfo = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));

            Arguments = new ReadOnlyCollection<ArgumentPair>(arguments.ToList());
        }

        public T Invoke<T>(object obj)
        {
            var parameters = MethodInfo.GetParameters();

            if(parameters.Any())
            {
                var values = new object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    var param = parameters[i];
                    var argument = Arguments.Single(arg => arg.Name.Equals(param.Name, StringComparison.OrdinalIgnoreCase));

                    //Erro interessante de se cometer no curso:
                    //   values[i] = argument.Value;
                    values[i] = Convert.ChangeType(argument.Value, param.ParameterType);
                }

                return (T)MethodInfo.Invoke(obj, values);
            }
            else
            {
                return (T)MethodInfo.Invoke(obj, null);
            }

        }
    }
}
