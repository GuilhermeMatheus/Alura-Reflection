using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure.IoC
{
    public class SimpleContainer : IContainer
    {
        private readonly Dictionary<Type, Type> _typeMap = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, object> _cache = new Dictionary<Type, object>();

        public void Register(Type from, Type to)
        {
            if (_typeMap.ContainsKey(from))
                throw new InvalidOperationException("Tipo já mapeado!");

            ValidateHierarchyOrThrow(from, to);

            _typeMap.Add(from, to);
        }

        private void ValidateHierarchyOrThrow(Type from, Type to)
        {
            if (from.IsInterface)
            {
                var typeImplementsInterface = to.GetInterfaces().Contains(from);
                if (!typeImplementsInterface)
                    throw new InvalidOperationException("Não é possível mapear uma interface a um tipo que não a implementa!");
            }
            else
            {
                var isSubclass = to.IsSubclassOf(from);
                if (!isSubclass)
                    throw new InvalidOperationException("Não é possível mapear uma classe que não é filha da outra!");
            }
        }

        public object Retrieve(Type type)
        {
            if (_cache.ContainsKey(type))
                return _cache[type];

            if (_typeMap.ContainsKey(type))
            {
                var targetType = _typeMap[type];
                return Retrieve(targetType);
            }

            var ctors = type.GetConstructors();
            var noParametersCtor = ctors.FirstOrDefault(ctor => ctor.GetParameters().Length == 0);

            if (noParametersCtor != null)
            {
                var dependency = noParametersCtor.Invoke(null);
                return dependency;
            }
            else
            {
                var ctor = ctors[0];
                var ctorParameters = ctor.GetParameters();
                var args = new object[ctorParameters.Length];

                for (int i = 0; i < ctorParameters.Length; i++)
                {
                    var param = ctorParameters[i];
                    args[i] = Retrieve(param.ParameterType);
                }

                var dependency = ctor.Invoke(args);
                return dependency;
            }
        }
    }
}
