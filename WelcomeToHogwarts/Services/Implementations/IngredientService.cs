using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Repositories.Interfaces;
using WelcomeToHogwarts.Services.Interfaces;

namespace WelcomeToHogwarts.Services.Implementations
{
    public class IngredientService : IIngredientService
    {
        private readonly IGenericRepository<Ingredient> _repository;

        public IngredientService(IGenericRepository<Ingredient> repository)
        {
            _repository = repository;
        }
        public async Task<Ingredient> AddIngredient(Ingredient ingredient)
        {
            return await _repository.Add(ingredient);
        }

        public async Task<List<Ingredient>> GetAllIngredients()
        {
            return await _repository.GetAll();
        }

        public async Task<Ingredient> GetIngredientById(Guid id)
        {
            return await _repository.GetById(id);
        }
    }
}
