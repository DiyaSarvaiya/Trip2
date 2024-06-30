using System.ComponentModel.DataAnnotations;

namespace Trip2.Areas.Admin.CustomValidation
{
    
    public class NotEqualAttribute : ValidationAttribute
    {
        private readonly string _otherProperty;

        public NotEqualAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherProperty);

            if (otherPropertyInfo == null)
            {
                return new ValidationResult($"Property {_otherProperty} not found.");
            }

            var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

            if (value == null || otherValue == null || !value.Equals(otherValue))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"{validationContext.DisplayName} cannot be equal to {otherPropertyInfo.Name}.");
        }
    }
}


