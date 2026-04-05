using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IDepartmentRepo:IRepository<Department>
    {
        Department? GetByIdWithDetails(int Id);


    }
}
