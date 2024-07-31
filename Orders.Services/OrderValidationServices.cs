
namespace Orders.Services
{
    public class OrderValidationServices : IOrderValidationServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderValidationServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ValidateOrder(OrderDto order)
        {
            foreach (var item in order.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetAsync(item.ProductId);

                if (product is null)
                    continue;
                if (product.Stock < item.Quantity)
                    return false;

            }
            return true;
        }


    }

}

