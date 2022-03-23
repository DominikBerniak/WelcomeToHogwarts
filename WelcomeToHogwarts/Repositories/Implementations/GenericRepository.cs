using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WelcomeToHogwarts.Persistance;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Repositories.Interfaces;

namespace WelcomeToHogwarts.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        protected readonly HogwartsContext _context;

        public GenericRepository(HogwartsContext context)
        {
            _context = context;
        }
        public virtual async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public virtual async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                   .Where(predicate)
                   .ToListAsync();
        }

        public virtual async Task<T> GetByProperty(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                   .Where(predicate)
                   .FirstOrDefaultAsync();
        }

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Edit(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
