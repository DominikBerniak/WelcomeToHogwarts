using WelcomeToHogwarts.Persistance.DataTransferObjects;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Repositories.Interfaces;
using WelcomeToHogwarts.Services.Interfaces;

namespace WelcomeToHogwarts.Services.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IPotionRepository _potionRepository;

        public RecipeService(IRecipeRepository recipeRepository, IPotionRepository potionRepository)
        {
            _recipeRepository = recipeRepository;
            _potionRepository = potionRepository;
        }
        public async Task<List<Recipe>> GetAllRecipes()
        {
            return await _recipeRepository.GetAllRecipes();
        }

        public async Task<GetRecipesStatusDto> GetMatchingRecipes(Guid potionId)
        {
            var recipeStatus = new GetRecipesStatusDto();
            var potion = await _potionRepository.GetPotionByProperty(p => p.Id == potionId);
            if (potion is null)
            {
                recipeStatus.Status = Status.NotFound;
                recipeStatus.StatusMessage = "No such potion";
                return recipeStatus;
            }

            var recipes = await _recipeRepository.GetAllRecipes();
            var matchingRecipes = new List<Recipe>();
            foreach (var recipe in recipes)
            {
                if (!potion.Ingredients.Except(recipe.Ingredients).Any())
                {
                    matchingRecipes.Add(recipe);
                }
            }
            if (matchingRecipes.Count == 0)
            {
                recipeStatus.Status = Status.NotFound;
                recipeStatus.StatusMessage = "No matching recipes found for the potion";
                return recipeStatus;
            }
            recipeStatus.Status = Status.Ok;
            recipeStatus.Recipes = matchingRecipes;
            return recipeStatus;
        }
    }
}
