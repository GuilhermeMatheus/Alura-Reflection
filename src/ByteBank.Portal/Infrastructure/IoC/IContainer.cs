using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure.IoC
{
    public interface IContainer
    {
        void Register(Type from, Type to);
        object Retrieve(Type type);
    }
}
