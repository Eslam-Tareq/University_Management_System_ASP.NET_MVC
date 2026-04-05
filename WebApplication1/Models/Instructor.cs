using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Instructor
    {
        [Key]
        public int Ins_Id { set; get; }
        public string Name { set; get; }

        public int Age { set; get; }
        public float Salary { set; get; }
        public string Degree { set; get; }

        public string Email { set; get; }
        public string Address { set; get; }
        [ForeignKey("Department")]
        public int Dept_Id { set; get; }


        public Department Department { set; get; }
        public ICollection<InsCourse>? InsCourses { set; get; } = new HashSet<InsCourse>();

    }
}
