using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Course
{
    public class CourseUpdateViewModel:CourseFormViewModel
    {
        [Display(Name ="Course ID")]
        public int Crs_Id { set; get; }
    }
}
