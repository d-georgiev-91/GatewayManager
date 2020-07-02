using System.Linq;
using System.Threading.Tasks;

namespace GatewayManager.Data
{
    public class DataService<TEntity> : IDataService<TEntity> where TEntity : class
    {
        protected GatewayManagerDbContext DbContext { get; }

        public DataService(GatewayManagerDbContext dbContext) => DbContext = dbContext;

        public async Task Add(TEntity entity) => await DbContext.Set<TEntity>().AddAsync(entity);

        public IQueryable<TEntity> GetAll() => DbContext.Set<TEntity>();

        public async Task<TEntity> GetByIdAsync(object id) => await DbContext.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity) => DbContext.Set<TEntity>().Update(entity);

        public async Task Delete(object id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                return;
            }

            DbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<int> SaveChangesAsync() => await DbContext.SaveChangesAsync();
    }
}