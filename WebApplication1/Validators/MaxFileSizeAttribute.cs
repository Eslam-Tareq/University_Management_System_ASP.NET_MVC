using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Validators
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null && file.Length > _maxFileSize)
            {
                return new ValidationResult($"the maximum size is  {_maxFileSize / 1024 / 1024} MB.");
            }
            return ValidationResult.Success;
        }
    }
}
