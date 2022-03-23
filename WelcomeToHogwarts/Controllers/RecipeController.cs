using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WelcomeToHogwarts.Persistance;
using WelcomeToHogwarts.Persistance.DataTransferObjects;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Services.Interfaces;

namespace WelcomeToHogwarts.Controllers
{
    [ApiController, Route("/recipes")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _service;

        public RecipeController(IRecipeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Recipe>>> GetAllRecipes()
        {
            var recipes = await _service.GetAllRecipes();
            if (recipes.Count == 0)
            {
                return NotFound("No recipes");
            }
            return Ok(recipes);
        }
        [HttpGet("{potionId}/help")]
        public async Task<ActionResult<List<Recipe>>> GetMatchingRecipes(Guid potionId)
        {
            var recipeStatus = await _service.GetMatchingRecipes(potionId);
            if (recipeStatus.Status == Status.NotFound)
            {
                return NotFound(recipeStatus.StatusMessage);
            }
            return Ok(recipeStatus.Recipes);
        }
    }
}
