using Microsoft.EntityFrameworkCore;
using Orders.Core.Entities;
using Orders.Core.Repository.Contract;
using Orders.Repository._Data;

namespace Orders.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly OrderDbContext _context;

        public GenericRepository(OrderDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public void Update(T entity)
        {
           _context.Update(entity);
        }
        public void Delete(T entity)
        {
          _context.Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();   
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

   
    }
    
}
