using System.Linq.Expressions;

namespace WelcomeToHogwarts.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> GetByProperty(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task<T> Delete(T entity);
        Task<T> Edit(T entity);
    }
}
