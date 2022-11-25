using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionable.Sample.Options
{
    public class AttributeOption : IOption
    {
        public bool UseAttribute { get; set; } = false;
    }
}
