using FluentValidation;
using Optionable.Sample.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionable.Sample
{
    public class CustomerValidator : OptionableValidator<Customer>
    {
        public CustomerValidator()
        {

        }
    }
}
