using System.ComponentModel.DataAnnotations;
using WebApplication1.Context;

namespace WebApplication1.Validators
{
    public class DepartIsExistsAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
           
            int ?id = value as int?;
            var db = (AppDbContext?)validationContext.GetService(typeof(AppDbContext));

            if (db == null) return ValidationResult.Success;
            var department = db.Departments.FirstOrDefault(d => d.Dept_Id == id);
            if (department is null) return new ValidationResult("department not found");
            else return ValidationResult.Success;
        }
    }
}
