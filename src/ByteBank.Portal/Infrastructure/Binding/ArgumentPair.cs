using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infrastructure.Binding
{
    public class ArgumentPair
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public ArgumentPair(string argumentName, string argumentValue)
        {
            Name = argumentName ?? throw new ArgumentNullException(nameof(argumentName));
            Value = argumentValue ?? throw new ArgumentNullException(nameof(argumentValue));
        }
    }
}
