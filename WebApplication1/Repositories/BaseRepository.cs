using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class BaseRepository<T> :IRepository<T> where T :class
    {
        private AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public List<T> GetAll()
        {
            return _dbSet.ToList();

        }

        public T? GetById(int Id)
        {
            return _dbSet.Find(Id);
        }
     
        public bool Any(Func<T, bool> predicate)
        {
            return _dbSet.Any(predicate);
        }

     
        public void Add(T student)
        {
            _dbSet.Add(student);
            _context.SaveChanges();
        }
        public void Update(T updatedT)
        {
            _dbSet.Update(updatedT);
            _context.SaveChanges();
        }
    }
}
