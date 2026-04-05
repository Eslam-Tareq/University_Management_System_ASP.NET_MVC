using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IRepository<T> where T : class
    {

        public List<T> GetAll();


        public T? GetById(int Id);


        public bool Any(Func<T, bool> predicate);




        public void Add(T t);

        public void Update(T t);
     
    }
}
