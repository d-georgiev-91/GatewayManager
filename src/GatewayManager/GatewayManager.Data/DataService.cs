using System.Linq;
using System.Threading.Tasks;

namespace GatewayManager.Data
{
    public class DataService<TEntity> : IDataService<TEntity> where TEntity : class
    {
        private readonly GatewayManagerDbContext _dbContext;

        public DataService(GatewayManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll() => _dbContext.Set<TEntity>();

        public async Task<TEntity> GetByIdAsync(object id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public async Task Delete(object id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                return;
            }

            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}