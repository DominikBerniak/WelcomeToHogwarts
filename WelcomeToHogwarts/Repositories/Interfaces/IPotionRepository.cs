using System.Linq.Expressions;
using WelcomeToHogwarts.Persistance.Entities;

namespace WelcomeToHogwarts.Repositories.Interfaces
{
    public interface IPotionRepository : IGenericRepository<Potion>
    {
        Task<List<Potion>> GetAllPotions();
        Task<List<Potion>> GetAllPotionsByProperty(Expression<Func<Potion, bool>> predicate);
        Task<Potion> GetPotionByProperty(Expression<Func<Potion, bool>> predicate);
    }
}
