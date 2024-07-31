

namespace Orders.Core.Repository.Contract
{
    public interface IUnitOfWork :IAsyncDisposable
    {

      IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> CompleteAsync();
    }
}
