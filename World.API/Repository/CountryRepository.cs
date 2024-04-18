using Microsoft.EntityFrameworkCore;
using World.API.Datas;
using World.API.Models;
using World.API.Repository.IRepository;

namespace World.API.Repository
{
    public class CountryRepository :GenericRepository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public CountryRepository(ApplicationDbContext dbcontext):base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task Update(Country entity)
        {
            _dbcontext.Countries.Update(entity);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
