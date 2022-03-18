using MartianRobots.Entities;
using MartianRobots.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MartianRobots.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext context;
        internal DbSet<T> dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(ApplicationDbContext context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            this._logger = logger;

        }

        public async Task<IEnumerable<T>> All()
        {
            this._logger.LogInformation($"Getting all entities of the class {nameof(T)}");
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            this._logger.LogInformation($"Get the entity of class {nameof(T)} by the id {id}");
            return await dbSet.FindAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            this._logger.LogInformation($"Add the entity of class {nameof(T)} to the database");
            var result = await dbSet.AddAsync(entity);
            return result.Entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            this._logger.LogInformation($"Delete the entity of class {nameof(T)} with the id {id} from the database");
            var entity = await dbSet.FindAsync(id);

            if(entity == null) return false;

            dbSet.Remove(entity);
            return true;
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }
    }
}
