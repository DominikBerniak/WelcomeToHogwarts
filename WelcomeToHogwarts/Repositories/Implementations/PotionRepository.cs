using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WelcomeToHogwarts.Persistance;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Repositories.Interfaces;

namespace WelcomeToHogwarts.Repositories.Implementations
{
    public class PotionRepository : GenericRepository<Potion>, IPotionRepository
    {
        public PotionRepository(HogwartsContext context) : base(context)
        {
        }

        public async Task<List<Potion>> GetAllPotions()
        {
            return await _context.Potion
                .Include(p => p.Maker)
                .Include(p => p.Ingredients)
                .Include(p => p.Recipe)
                .ThenInclude(r => r.Ingredients)
                .ToListAsync();
        }

        public async Task<List<Potion>> GetAllPotionsByProperty(Expression<Func<Potion, bool>> predicate)
        {
            return await _context.Potion
                .Where(predicate)
                .Include(p => p.Maker)
                .Include(p => p.Ingredients)
                .Include(p => p.Recipe)
                .ToListAsync();
        }

        public async Task<Potion> GetPotionByProperty(Expression<Func<Potion, bool>> predicate)
        {
            return await _context.Potion
                .Where(predicate)
                .Include(p => p.Maker)
                .Include(p => p.Ingredients)
                .Include(p => p.Recipe)
                .FirstOrDefaultAsync();
        }
    }
}
