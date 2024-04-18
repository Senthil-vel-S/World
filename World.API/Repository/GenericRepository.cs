using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using World.API.Datas;
using World.API.Repository.IRepository;

namespace World.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbcontext;
        public GenericRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task Create(T entity)
        {
            await _dbcontext.AddAsync(entity);
            await Save();
        }

        public async Task Delete(T entity)
        {
            _dbcontext.Remove(entity);
            await Save();
        }

        public async Task<T> Get(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public bool IsRecordExists(Expression<Func<T, bool>> condition)
        {
            var result = _dbcontext.Set<T>().AsQueryable().Where(condition).Any();
            return result;
        }

        public async Task Save()
        {
            await _dbcontext.SaveChangesAsync();
        }
    }
}
