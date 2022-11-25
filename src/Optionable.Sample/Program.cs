using FluentValidation.Results;
using Optionable.Sample.Options;
using System;

namespace Optionable.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();
            CustomerValidator validator = new CustomerValidator();
            validator.AddOption<AttributeOption>(opt => opt.UseNotNullAttribute = true);

            ValidationResult results = validator.Validate(customer);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }

            Console.ReadLine();
        }
    }
}
