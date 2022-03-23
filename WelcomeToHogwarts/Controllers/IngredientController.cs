using Microsoft.AspNetCore.Mvc;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Services.Interfaces;

namespace WelcomeToHogwarts.Controllers
{
    [ApiController, Route("/ingredients")]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _service;

        public IngredientController(IIngredientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ingredient>>> GetAllIngredients()
        {
            var ingredients = await _service.GetAllIngredients();
            if (ingredients.Count == 0)
            {
                return NotFound("No ingredients");
            }
            var sortedIngredients = ingredients.OrderBy(i => i.Name);
            return Ok(sortedIngredients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredientById(Guid id)
        {
            var ingredient = await _service.GetIngredientById(id);
            if (ingredient is null)
            {
                return NotFound("No such ingredient");
            }
            return Ok(ingredient);
        }

        [HttpPost]
        public async Task<ActionResult<Ingredient>> AddIngredient(Ingredient ingredient)
        {
            var createdIngredient = await _service.AddIngredient(ingredient);
            return Created($"/ingredients/{createdIngredient.Id}", createdIngredient);
        }
    }
}
