using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Student
{
    public class StudentUpdateViewModel:StudentFormViewModel
    {
        [Display(Name ="Current Image")]
      new  public string? Image { set; get; }
    }
}
