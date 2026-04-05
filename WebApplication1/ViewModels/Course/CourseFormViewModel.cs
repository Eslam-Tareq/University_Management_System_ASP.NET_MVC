using System.ComponentModel.DataAnnotations;
using WebApplication1.Validators;

namespace WebApplication1.ViewModels.Course
{
    public class CourseFormViewModel
    {
        [Required]
        [StringLength (100,MinimumLength =3 ,ErrorMessage ="course name must be in range 3 to 20 char")]
        public string Name { set; get; }
        [Required]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "course name must be in range 10 to 200 char")]

        public string Description { set; get; }
        [Required(ErrorMessage ="topics must be entered seperated by commas")]

        public string Topics { get; set; }
        [Required]
        [LessThanOrEqual(comparisonProperty:"Grade" ,ErrorMessage ="min degree must be lessthan or equal Grade")]
        public float? Mindegree { set; get; }
        [Required]
        public float? Grade { set; get; }




    }
}
