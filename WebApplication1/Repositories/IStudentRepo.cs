using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IStudentRepo:IRepository<Student>
    {
        Student? GetByIdWithDetails(int Id);


         Student? GetByEmail(string email);
       
    }
}
