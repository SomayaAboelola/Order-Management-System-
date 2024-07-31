

namespace Orders.Services
{
    public interface IOrderValidationServices
    {
        Task<bool> ValidateOrder(OrderDto order);
       


    }
}
