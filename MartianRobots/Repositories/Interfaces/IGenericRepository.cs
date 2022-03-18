using System.Linq.Expressions;

namespace MartianRobots.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<T> GetById(Guid id);
        Task<T> Add(T entity);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}
