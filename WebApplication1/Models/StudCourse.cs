using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class StudCourse
    {
        [ForeignKey("Student")]
        public int Std_Id { set; get; }
        public Student Student { set; get; }
        [ForeignKey("Course")]
        public int Crs_Id { set; get; }

        public Course Course { set; get; }

        public float Grade { set; get; }
    }
}
