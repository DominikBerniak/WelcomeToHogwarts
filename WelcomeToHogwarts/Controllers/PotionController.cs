using Microsoft.AspNetCore.Mvc;
using WelcomeToHogwarts.Persistance.DataTransferObjects;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Services.Interfaces;

namespace WelcomeToHogwarts.Controllers
{
    [ApiController, Route("/potions")]
    public class PotionController : ControllerBase
    {
        private readonly IPotionService _service;

        public PotionController(IPotionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Potion>>> GetAllPotions()
        {
            var potions = await _service.GetAllPotions();
            if (potions.Count == 0)
            {
                return NotFound("No potions");
            }
            return Ok(potions);
        }
        [HttpGet("search/{potionName}")]
        public async Task<ActionResult<Potion>> GetPotionByName(string potionName)
        {
            var potion = await _service.GetPotionByName(potionName);
            if (potion is null)
            {
                return NotFound("No such potion");
            }
            return Ok(potion);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Potion>>> GetPotionsBySearchQuery([FromQuery] string search)
        {
            var potions = await _service.GetPotionsBySearchQuery(search);
            if (potions.Count == 0)
            {
                return NotFound("No potions matched the search results");
            }
            return Ok(potions);
        }

        [HttpGet("students/{studentId}")]
        public async Task<ActionResult<List<Potion>>> GetPotionsByStudentId(Guid studentId)
        {
            var potions = await _service.GetPotionsByStudentId(studentId);
            if (potions.Count == 0)
            {
                return NotFound("No potions matched the search results");
            }
            return Ok(potions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Potion>>> GetPotionById(Guid id)
        {
            var potion = await _service.GetPotionById(id);
            if (potion is null)
            {
                return NotFound("No potion matched the search results");
            }
            return Ok(potion);
        }

        [HttpPost]
        public async Task<ActionResult<Potion>> AddPotion(CreatePotionDto potionData)
        {
            var createdPotionStatus = await _service.AddPotion(potionData);
            if (createdPotionStatus.Status == Status.BadRequest)
            {
                return BadRequest(createdPotionStatus.StatusMessage);
            }
            return Created($"/potions/{createdPotionStatus.Potion.Id}", createdPotionStatus.Potion);
        }


        [HttpPost("brew")]
        public async Task<ActionResult<Potion>> BrewPotion(BasePotionDto potionData)
        {
            var createdPotionStatus = await _service.BrewPotion(potionData);
            if (createdPotionStatus.Status == Status.NotFound)
            {
                return BadRequest(createdPotionStatus.StatusMessage);
            }
            return Created($"/potions/{createdPotionStatus.Potion.Id}", createdPotionStatus.Potion);
        }


        [HttpPut("{potionId}/add")]
        public async Task<ActionResult<Potion>> AddIngredientToPotion(Guid potionId, IngredientDto ingredientData)
        {
            var potionStatus = await _service.AddIngredientToPotion(potionId, ingredientData);
            if (potionStatus.Status == Status.NotFound)
            {
                return NotFound(potionStatus.StatusMessage);
            }
            if (potionStatus.Status == Status.BadRequest)
            {
                return BadRequest(potionStatus.StatusMessage);
            }
            return Ok(potionStatus.Potion);
        }

        [HttpGet("recipes/{recipeId}")]
        public async Task<ActionResult<List<Potion>>> GetPotionsForRecipeId(Guid recipeId)
        {
            var potionsStatus = await _service.GetPotionsByRecipeId(recipeId);
            if (potionsStatus.Status == Status.NotFound)
            {
                return NotFound(potionsStatus.StatusMessage);
            }
            return Ok(potionsStatus.Potions);
        }
    }
}
