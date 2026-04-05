using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Student
    {
        [Key]
        public int SSN { set; get; }
        [Display(Name ="full name")]
        [Required(ErrorMessage ="full name is required")]
        public string Name { set; get; }
        [Range(22,40,ErrorMessage =$"age must in range (22,40)")]
        public int Age { set; get; }
        public string? Address { set; get; }
        [Required]
        [EmailAddress(ErrorMessage ="Email address is invalid")]
        public string Email { set; get; }
        public string? Image { set; get; } = "defualt.png";
        [Required]
        public float Grade { set; get; }
        [ForeignKey("Department")]
        [Required]
        public int Dept_Id { set; get; }

        public Department Department { set; get; }

        public IList<StudCourse>? StudCourses { set; get; } = new List<StudCourse>();
     }
}
