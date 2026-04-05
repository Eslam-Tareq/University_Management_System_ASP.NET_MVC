using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Validators
{
    public class LessThanOrEqualAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public LessThanOrEqualAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
                return new ValidationResult($"Property {_comparisonProperty} not found.");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);

          
            if (comparisonValue == null) return ValidationResult.Success;

            if (float.TryParse(value.ToString(), out float current) &&
                float.TryParse(comparisonValue.ToString(), out float target))
            {
                if (current > target)
                {
                    return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be less than or equal to {target}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
