using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionable.Exceptions
{
    public class OptionNotFoundException : Exception
    {
        public OptionNotFoundException() : base($"Option not found in selected section")
        {

        }
    }
}
