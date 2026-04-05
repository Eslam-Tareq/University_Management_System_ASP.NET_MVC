using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class DepartmentRepo:BaseRepository<Department>,IDepartmentRepo
    {
        private AppDbContext _context;
        public DepartmentRepo(AppDbContext context):base(context)
        {
            _context = context;
        }
       

        public Department? GetByIdWithDetails(int Id)
        {
            return _context.Departments.Include(d => d.Students).FirstOrDefault(d => d.Dept_Id == Id);
        }
       
       

       
    }
}
