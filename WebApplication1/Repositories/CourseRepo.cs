using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class CourseRepo:BaseRepository<Course>  ,ICourseRepo
    {

        private readonly AppDbContext _context;

        public CourseRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
