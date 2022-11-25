# Optionable
Add options pattern to your project

## Sample

### Add an Option to FluentValidation Library
I want to added attribute option instead of RuleFor() method.

Option defining
``` csharp
  public class AttributeOption : IOption
  {
      public bool UseAttribute { get; set; } = false;
  }
```

Attribute defining If my option is active I will use this attribute
``` csharp
  [AttributeUsage(AttributeTargets.Property)]
  public class NotNullAttribute : Attribute
  {
  }
```

Classic FluentValidation sample class
``` csharp
    public class Customer
    {        
        public int Id { get; set; }
        //My attribute
        [NotNull]
        public string Surname { get; set; }
        public string Forename { get; set; }
        public decimal Discount { get; set; }
        //My attribute
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
            //Get injected option
            var option = this.GetOption<AttributeOption>();
            if(option is not null && option.UseAttribute)
            {
                var props = context.InstanceToValidate.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (prop.GetCustomAttribute<NotNullAttribute>() is not null)
                    {
                        var param = Expression.Parameter(typeof(T));
                        var lambda = Expression.Lambda<Func<T, object>>((Expression)Expression.Property(param, prop.Name), param);

                        this.RuleFor<object>(lambda).NotNull();
                    }
                }
            }         
            
            return base.Validate(context);
        }
    }
```

### Final Usage
``` csharp
  Customer customer = new Customer();
  CustomerValidator validator = new CustomerValidator();
  validator.AddOption<AttributeOption>(opt => opt.UseAttribute = true);

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
Property Address failed validation. Error was: 'Address' boş olamaz.
```



