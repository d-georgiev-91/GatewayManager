using System.Linq;
using System.Threading.Tasks;

namespace GatewayManager.Data
{
    public interface IDataService<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        IQueryable<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(object id);

        void Update(TEntity entity);

        Task Delete(object id);

        Task<int> SaveChangesAsync();
    }
}
