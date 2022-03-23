using Microsoft.EntityFrameworkCore;
using WelcomeToHogwarts.Persistance;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Repositories.Interfaces;

namespace WelcomeToHogwarts.Repositories.Implementations
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(HogwartsContext context) : base(context)
        {
        }

        public async Task<List<Recipe>> GetAllRecipes()
        {
            return await _context.Recipe
                .Include(r => r.Maker)
                .Include(r => r.Ingredients)
                .ToListAsync();
        }
    }
}
