

namespace Orders.Core.Service.Contract
{
    public interface IOrderServices
    {
        Task<OrderResultDto?> CreateOrderAsync(OrderDto input);
        Task<OrderResultDto?> UpdateOrderAsync(int orderId ,OrderDto input);
        Task<IReadOnlyList<OrderResultDto>> GetOrderForUser();
        Task<OrderResultDto> GetOrderForUserById(int orderId); 
        
        // Task<OrderResultDto?> CreateOrUpdateOrderAsync(OrderDto input);
    }      

}
