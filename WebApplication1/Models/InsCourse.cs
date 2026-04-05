using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class InsCourse
    {
        [ForeignKey("Instructor")]
        public int Ins_Id { set; get; }
        public Instructor Instructor { set; get; }
        [ForeignKey("Course")]
        public int Crs_Id { set; get; }

        public Course Course { set; get; }

        public int Hours { set; get; }
    }
}
