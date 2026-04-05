using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Course
    {
        [Key]
        public int Crs_Id { set; get; }

        public string Name { set; get; }
        public string Description { set; get; }

        public List<string> Topics { get; set; } = new();
        public float ? Grade { set; get; }
        public float? Mindegree { set; get; }
        public ICollection<StudCourse>? StudCourses { set; get; } = new HashSet<StudCourse>();
        public ICollection<InsCourse>? InsCourses { set; get; } = new HashSet<InsCourse>();


    }
}
