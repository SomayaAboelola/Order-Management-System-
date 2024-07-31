
using Microsoft.EntityFrameworkCore;

using Orders.Repository._Data;


namespace Orders.Services
{
    public class ProductServices : IProductServices
    {
        private readonly OrderDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServices(OrderDbContext context,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public async Task<Product?> CreateProduct(Product input)
        {

            var product = _unitOfWork.Repository<Product>().AddAsync(input);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0)
                return null;

            return input;

        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        => await _unitOfWork.Repository<Product>().GetAllAsync();


        public async Task<Product?> GetProductById(int id)
        {
            var product = await _unitOfWork.Repository<Product>().GetAsync(id);
            if (product is null)
                return null;
            return product;
        }


        public async Task<bool> ProductExistsAsync(int productId)
        {
            var ProductExist = await _context.Products.AnyAsync(p => p.Id == productId);

            if (ProductExist is false)
                return false;
            return true;
        }

        public async Task UpdateProduct(int productId, Product input)
        {
            // 1. Get the existing product
            var product = await _unitOfWork.Repository<Product>().GetAsync(productId);

            // 2. Check if product exists (optional, can be done before calling UpdateProductAsync)
            if (product is null)
                throw new Exception("Product not found for update");

            // 3. Update product properties 
            product.Name = input.Name;
            product.Price = input.Price;
            product.Stock = input.Stock;

            // 4. Save updated product to data store
            _unitOfWork.Repository<Product>().Update(product);
            await _unitOfWork.CompleteAsync();

        }


    }
}
