

namespace Orders.Core.Service.Contract
{
    public interface IProductServices 
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductById(int id);
        Task<Product?> CreateProduct(Product product);
        Task UpdateProduct(int productId, Product product);
        Task<bool> ProductExistsAsync(int productId);



    }
}
