using Microsoft.EntityFrameworkCore;
using WelcomeToHogwarts.Persistance.DataTransferObjects;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Persistance.Enums;
using WelcomeToHogwarts.Repositories.Interfaces;
using WelcomeToHogwarts.Services.Interfaces;

namespace WelcomeToHogwarts.Services.Implementations
{
    public class PotionService : IPotionService
    {
        private readonly IPotionRepository _potionRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IGenericRepository<Ingredient> _ingredientRepository;

        public PotionService(IPotionRepository potionRepository, IRecipeRepository recipeRepository
            , IGenericRepository<Student> studentRepository, IGenericRepository<Ingredient> ingredientRepository)
        {
            _potionRepository = potionRepository;
            _recipeRepository = recipeRepository;
            _studentRepository = studentRepository;
            _ingredientRepository = ingredientRepository;
        }
        public async Task<CreatePotionStatusDto> AddIngredientToPotion(Guid potionId, IngredientDto ingredientData)
        {
            var potionStatus = new CreatePotionStatusDto();
            var potion = await _potionRepository.GetPotionByProperty(p => p.Id == potionId);
            if (potion is null)
            {
                potionStatus.Status = Status.NotFound;
                potionStatus.StatusMessage = "No such potion";
                return potionStatus;
            }
            if (potion.Ingredients.Count == 5)
            {
                potionStatus.Status = Status.BadRequest;
                potionStatus.StatusMessage = "Potion is already done";
                return potionStatus;
            }
            var ingredient = await _ingredientRepository.GetByProperty(i => i.Name == ingredientData.Name);
            if (ingredient is null)
            {
                var newIngredient = new Ingredient
                {
                    Name = ingredientData.Name,
                };
                newIngredient = await _ingredientRepository.Add(newIngredient);
                potion.Ingredients.Add(newIngredient);
            }
            else
            {
                potion.Ingredients.Add(ingredient);
            }
            if (potion.Ingredients.Count < 5)
            {
                potion = await _potionRepository.Edit(potion);
                potionStatus.Status = Status.Updated;
                potionStatus.StatusMessage = "Successfully added a ingredient to this potion";
                potionStatus.Potion = potion;
                return potionStatus;
            }
            var recipes = await _recipeRepository.GetAllRecipes();
            bool areEqual = false;
            var potionIngredientsIds = potion.Ingredients.OrderBy(i => i.Id).Select(i => i.Id).ToList();
            foreach (var recipe in recipes)
            {
                var recipeIngredientsIds = recipe.Ingredients.OrderBy(i => i.Id).Select(i => i.Id).ToList();
                if (recipe.Ingredients.Count == potion.Ingredients.Count && recipeIngredientsIds.SequenceEqual(potionIngredientsIds))
                {
                    areEqual = true;
                    potion.Recipe = recipe;
                    potion.BrewingStatus = BrewingStatus.Replica;
                    break;
                }
            }
            var maker = potion.Maker;
            if (!areEqual)
            {
                var studentRecipesCount = recipes.Where(r => r.Maker.Id == maker.Id).ToList().Count;
                var newRecipe = new Recipe
                {
                    Name = $"{maker.Name}'s Discovery #{studentRecipesCount + 1}",
                    Maker = maker,
                    Ingredients = potion.Ingredients
                };
                newRecipe = await _recipeRepository.Add(newRecipe);

                potion.BrewingStatus = BrewingStatus.Discovery;
                potion.Recipe = newRecipe;
            }
            potion = await _potionRepository.Edit(potion);
            potionStatus.Status = Status.Ok;
            potionStatus.StatusMessage = "Your potions is done";
            potionStatus.Potion = potion;
            return potionStatus;
        }

        public async Task<CreatePotionStatusDto> AddPotion(CreatePotionDto potionData)
        {
            var createPotionStatus = new CreatePotionStatusDto();
            var recipes = await _recipeRepository.GetAllRecipes();
            var maker = await _studentRepository.GetByProperty(s => s.Name == potionData.MakerName);
            if (maker is null)
            {
                createPotionStatus.Status = Status.BadRequest;
                createPotionStatus.StatusMessage = "No such student";
                return createPotionStatus;
            }
            var potionIngredients = await _ingredientRepository.GetAll(i => potionData.IngredientsId.Contains(i.Id));
            Potion newPotion = new Potion
            {
                Name = potionData.PotionName,
                Maker = maker,
                Ingredients = potionIngredients
            };

            bool areEqual = false;
            var potionIngredientsIds = potionIngredients.OrderBy(i => i.Id).Select(i => i.Id).ToList();
            foreach (var recipe in recipes)
            {
                var recipeIngredientsIds = recipe.Ingredients.OrderBy(i => i.Id).Select(i => i.Id).ToList();
                if (recipe.Ingredients.Count == potionIngredients.Count && recipeIngredientsIds.SequenceEqual(potionIngredientsIds))
                {
                    areEqual = true;
                    newPotion.Recipe = recipe;
                    newPotion.BrewingStatus = BrewingStatus.Replica;
                    break;
                }
            }

            if (!areEqual)
            {
                var studentRecipesCount = recipes.Where(r => r.Maker.Id == maker.Id).ToList().Count;
                var newRecipe = new Recipe
                {
                    Name = $"{maker.Name}'s Discovery #{studentRecipesCount + 1}",
                    Maker = maker,
                    Ingredients = potionIngredients
                };

                newRecipe = await _recipeRepository.Add(newRecipe);
                newPotion.BrewingStatus = BrewingStatus.Discovery;
                newPotion.Recipe = newRecipe;
            }
            newPotion = await _potionRepository.Add(newPotion);
            createPotionStatus.Potion = newPotion;
            createPotionStatus.Status = Status.Created;
            createPotionStatus.StatusMessage = "Successfully created a new potion";
            return createPotionStatus;
        }

        public async Task<CreatePotionStatusDto> BrewPotion(BasePotionDto potionData)
        {
            var createPotionStatus = new CreatePotionStatusDto();
            var maker = await _studentRepository.GetByProperty(s => s.Name.ToLower() == potionData.MakerName.ToLower());
            if (maker is null)
            {
                createPotionStatus.Status = Status.NotFound;
                createPotionStatus.StatusMessage = "No such student";
                return createPotionStatus;
            }
            var newPotion = new Potion
            {
                Name = potionData.PotionName,
                Maker = maker,
                BrewingStatus = BrewingStatus.Brew
            };
            newPotion = await _potionRepository.Add(newPotion);
            createPotionStatus.Status = Status.Created;
            createPotionStatus.StatusMessage = "Successfully brewed a new potion";
            createPotionStatus.Potion = newPotion;
            return createPotionStatus;
        }

        public async Task<List<Potion>> GetAllPotions()
        {
            return await _potionRepository.GetAllPotions();
        }

        public async Task<Potion> GetPotionById(Guid id)
        {
            return await _potionRepository.GetPotionByProperty(p => p.Id == id);
        }

        public async Task<Potion> GetPotionByName(string potionName)
        {
            return await _potionRepository.GetPotionByProperty(p => p.Name == potionName);
        }

        public async Task<List<Potion>> GetPotionsBySearchQuery(string search)
        {
            return await _potionRepository
                .GetAllPotionsByProperty(p => p.Name.ToLower().Contains(search.ToLower())
                || p.Maker.Name.ToLower().Contains(search.ToLower()));
        }

        public async Task<List<Potion>> GetPotionsByStudentId(Guid studentId)
        {
            return await _potionRepository.GetAllPotionsByProperty(p => p.Maker.Id == studentId);
        }

        public async Task<GetPotionsStatusDto> GetPotionsByRecipeId(Guid recipeId)
        {
            var potionsStatus = new GetPotionsStatusDto();
            var potions = await _potionRepository.GetAllPotionsByProperty(p => p.Recipe.Id == recipeId);
            if (potions is null)
            {
                potionsStatus.Status = Status.NotFound;
                potionsStatus.StatusMessage = "No potions for given recipe";
                return potionsStatus;
            }
            potionsStatus.Status = Status.Ok;
            potionsStatus.Potions = potions;
            return potionsStatus;
        }
    }
}
