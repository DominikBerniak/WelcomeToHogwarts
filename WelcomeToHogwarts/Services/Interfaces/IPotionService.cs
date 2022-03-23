using Microsoft.AspNetCore.Mvc;
using WelcomeToHogwarts.Persistance.DataTransferObjects;
using WelcomeToHogwarts.Persistance.Entities;

namespace WelcomeToHogwarts.Services.Interfaces
{
    public interface IPotionService
    {
        Task<List<Potion>> GetAllPotions();
        Task<Potion> GetPotionByName(string potionName);
        Task<Potion> GetPotionById(Guid id);
        Task<List<Potion>> GetPotionsBySearchQuery(string search);
        Task<List<Potion>> GetPotionsByStudentId(Guid studentId);
        Task<CreatePotionStatusDto> AddPotion(CreatePotionDto potionData);
        Task<CreatePotionStatusDto> BrewPotion(BasePotionDto potionData);
        Task<CreatePotionStatusDto> AddIngredientToPotion(Guid potionId, IngredientDto ingredientData);
        Task<GetPotionsStatusDto> GetPotionsByRecipeId(Guid recipeId);
    }
}
