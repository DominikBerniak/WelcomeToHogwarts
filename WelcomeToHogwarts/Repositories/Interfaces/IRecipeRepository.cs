using WelcomeToHogwarts.Persistance.Entities;

namespace WelcomeToHogwarts.Repositories.Interfaces
{
    public interface IRecipeRepository : IGenericRepository<Recipe>
    {
        Task<List<Recipe>> GetAllRecipes();
    }
}
