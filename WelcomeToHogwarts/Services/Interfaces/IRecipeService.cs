using WelcomeToHogwarts.Persistance.DataTransferObjects;
using WelcomeToHogwarts.Persistance.Entities;

namespace WelcomeToHogwarts.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<List<Recipe>> GetAllRecipes();
        Task<GetRecipesStatusDto> GetMatchingRecipes(Guid potionId);
    }
}
