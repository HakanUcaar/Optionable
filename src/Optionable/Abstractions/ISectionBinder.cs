using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionable
{
    public interface ISectionBinder
    {
        public IConfigurationSection Section { get; }
        public IOptionable Source { get; }
    }

    public interface ISectionBinder<T> where T : IOptionable
    {
        public IConfigurationSection Section { get; }
        public T Source { get; }
    }
}
