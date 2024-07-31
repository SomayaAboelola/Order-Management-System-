using Orders.Core.Entities;
using Orders.Core.Entities.Basket;
using Orders.Core.Repository.Contract;
using StackExchange.Redis;
using System.Text.Json;

namespace Orders.Repository.BasketRepository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        {

            _database = connectionMultiplexer.GetDatabase();
        }
        public async Task<bool> DeleteBasket(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

     

        public async Task<CustomerBasket?> UpdateBasket(CustomerBasket customer)
        {
            var jsonBasket = JsonSerializer.Serialize(customer);
            var createdOrUpdated = await _database.StringSetAsync(customer.Id, jsonBasket, TimeSpan.FromDays(7));

            return await GetBasketAsync(customer.Id);
        }
    }
}
