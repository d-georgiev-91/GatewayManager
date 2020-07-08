using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GatewayManager.Data
{
    public interface IDataService<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        IQueryable<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(object id);

        IQueryable<TEntity> Filter<TProperty>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TProperty>> include = null);

        void Update(TEntity entity);

        Task Delete(object id);

        Task<int> SaveChangesAsync();
    }
}
