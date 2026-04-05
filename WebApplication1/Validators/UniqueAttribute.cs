using System.ComponentModel.DataAnnotations;
using WebApplication1.Context;
using WebApplication1.ViewModels.Student;

namespace WebApplication1.Validators
{
    public class UniqueAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
                var email = value as string;

            var db = (AppDbContext?)validationContext.GetService(typeof(AppDbContext));

            if (db == null) return ValidationResult.Success;
            var student = db.Students.FirstOrDefault(s => s.Email == email);
                if(student?.SSN ==(validationContext.ObjectInstance as StudentFormViewModel)?.SSN)
                    {
                         return ValidationResult.Success;
                    }
                if (student is not null) return new ValidationResult(errorMessage: "email already exists");
                else return ValidationResult.Success;
            

        }
    }
}
