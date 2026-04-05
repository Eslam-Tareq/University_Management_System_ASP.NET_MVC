using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Validators;

namespace WebApplication1.ViewModels.Student
{
    public class StudentFormViewModel
    {
        public int? SSN { set; get; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage="*")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "full name must be in range 3 to 20 character")]
        public string Name { set; get; }
        [Range(22, 40, ErrorMessage = $"age must in range (22,40)")]

        public int Age { set; get; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Address must be in range 3 to 30 character")]

        public string? Address { set; get; }
        [EmailAddress(ErrorMessage = "Email address is invalid")]
        //[Unique]
        [Remote("CheckEmailUnique","Student",ErrorMessage ="email already exists",AdditionalFields ="SSN")]
        public string Email { set; get; }
        
        public string? Image { set; get; }
        [Range(0,200,ErrorMessage ="student grade must be in 0 to 200 range")]
        public float Grade { set; get; }

        [Display(Name = "Department")]
        [Required]
        [DepartIsExists]
        public int Dept_Id { set; get; }

        [Display(Name = "Image")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg", ".bmp" }, ErrorMessage = "\".jpg\", \".jpeg\", \".png\", \".gif\", \".webp\", \".svg\", \".bmp\" is allowed")]
        [MaxFileSize(10*1024*1024)]
        public IFormFile? ImageFile { get; set; }
    }
}
