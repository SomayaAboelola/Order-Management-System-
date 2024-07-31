using Orders.Core.Entities.Basket;


namespace Orders.Core.Repository.Contract
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string id);
        Task<CustomerBasket?> UpdateBasket(CustomerBasket customer);
        Task<bool> DeleteBasket(string id);

    }
}
