using Microsoft.AspNetCore.Mvc;
using WelcomeToHogwarts.Persistance.Entities;

namespace WelcomeToHogwarts.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<List<Ingredient>> GetAllIngredients();
        Task<Ingredient> GetIngredientById(Guid id);
        Task<Ingredient> AddIngredient(Ingredient ingredient);
    }
}
