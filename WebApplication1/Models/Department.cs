using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Department
    {
        [Key]
        public int Dept_Id { set; get; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "department name must be in range 3 to 20 char")]

        public string Name { set; get; }
        [RegularExpression(@"^01[0125][0-9]{8}$")]
        public string PhoneNumber { set; get; }
        [StringLength(200, MinimumLength = 2, ErrorMessage = "location must be in range 3 to 50 char")]

        public string Location { set; get; }

        public ICollection<Student>? Students { set; get; } = new HashSet<Student>();

        public ICollection<Instructor>? Instructors { set; get; } = new HashSet<Instructor>();

    }
}
