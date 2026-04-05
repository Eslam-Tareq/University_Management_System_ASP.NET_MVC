using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Validators
{
    public class MinNumberAttribute : ValidationAttribute
    {
        private readonly double _min;

        public MinNumberAttribute(double min)
        {
            _min = min;
            ErrorMessage = "value must be geater than or equal";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (double.TryParse(value.ToString(), out double numericValue))
            {
                if (numericValue < _min)
                {
                    return new ValidationResult(string.Format(ErrorMessage, _min));
                }
                return ValidationResult.Success;
            }

            return new ValidationResult("invalid value");
        }
    }
}
