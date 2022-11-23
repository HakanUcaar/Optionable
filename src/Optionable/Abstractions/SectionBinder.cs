using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionable
{
    public class SectionBinder : ISectionBinder
    {
        private readonly IConfigurationSection _section;
        private readonly IOptionable _source;
        public SectionBinder(IOptionable source, IConfigurationSection section)
        {
            _section = section;
            _source = source;
        }
        public IConfigurationSection Section => _section;
        public IOptionable Source => _source;
    }
}
