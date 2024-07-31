

namespace Orders.Core.Repository.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetAsync(int id);   
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
