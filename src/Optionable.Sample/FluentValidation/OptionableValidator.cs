using FluentValidation;
using FluentValidation.Results;
using Optionable.Sample.Attributies;
using Optionable.Sample.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Optionable.Sample.FluentValidation
{
    public class OptionableValidator<T> : AbstractValidator<T>, IOptionable
    {
        public List<IOption> Options {get; set;} = new List<IOption>();
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            this.UseNotNullAttribute(context);
            return base.Validate(context);
        }

        private void UseNotNullAttribute(ValidationContext<T> context)
        {
            var option = this.GetOption<AttributeOption>();
            if (option is not null && option.UseNotNullAttribute)
            {
                var props = context.InstanceToValidate.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (prop.GetCustomAttribute<NotNullAttribute>() is not null)
                    {
                        var param = Expression.Parameter(typeof(T));
                        var lambda = Expression.Lambda<Func<T, object>>((Expression)Expression.Property(param, prop.Name), param);

                        var withMessage = prop.GetCustomAttribute<WithMessageAttribute>();
                        if (withMessage is not null)
                        {
                            this.RuleFor<object>(lambda).NotNull().WithMessage(withMessage.Message);
                        }
                        else
                        {
                            this.RuleFor<object>(lambda).NotNull();
                        }
                    }
                }
            }
        }
    }
}
