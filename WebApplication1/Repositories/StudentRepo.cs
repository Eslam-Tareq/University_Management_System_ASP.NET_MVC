using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class StudentRepo : BaseRepository<Student>, IStudentRepo
    {
        private readonly AppDbContext _context;

        public StudentRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public Student? GetByIdWithDetails(int id)
        {
            return _context.Students
                .Include(s => s.Department)
                .Include(s => s.StudCourses)
                    .ThenInclude(sc => sc.Course)
                .FirstOrDefault(s => s.SSN == id);
        }

        public Student? GetByEmail(string email)
        {
            return _context.Students.FirstOrDefault(s => s.Email == email);
        }
    }
}
