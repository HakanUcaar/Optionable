using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionable.Sample.Attributies
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class WithMessageAttribute : Attribute
    {
        public string Message { get; set; }
        public WithMessageAttribute(string message)
        {
            Message = message;
        }
    }
}
