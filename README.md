# Optionable
Add option ability to your class

## Sample

### Add an Option to FluentValidation Library
I want to added attribute option instead of RuleFor() method.
https://github.com/FluentValidation/FluentValidation

Option defining
``` csharp
  public class AttributeOption : IOption
  {
      public bool UseNotNullAttribute { get; set; } = false;
  }
```

Attribute defining If my option is active I will use this attribute
``` csharp
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullAttribute : Attribute
    {

    }
  
    [AttributeUsage(AttributeTargets.Property)]
    internal class WithMessageAttribute : Attribute
    {
        public string Message { get; set; }
        public WithMessageAttribute(string message)
        {
            Message = message;
        }
    }
```

Classic FluentValidation sample class
``` csharp
    public class Customer
    {        
        public int Id { get; set; }
        //My Attribute
        [NotNull]
        public string Surname { get; set; }
        //My Attribute
        [NotNull]
        [WithMessage("Please specify a first name")]
        public string Forename { get; set; }
        public decimal Discount { get; set; }
        //My Attribute
        [NotNull]
        public string Address { get; set; }
    }
```

Implement IOptionable interface your base class
``` csharp
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
            //Get injected option
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
```
``` csharp
    public class CustomerValidator : OptionableValidator<Customer>
    {
        public CustomerValidator()
        {

        }
    }
```

### Usage
``` csharp
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
```
Output
``` csharp
Property Surname failed validation. Error was: 'Surname' boş olamaz.
Property Forename failed validation. Error was: Please specify a first name
Property Address failed validation. Error was: 'Address' boş olamaz.
```



