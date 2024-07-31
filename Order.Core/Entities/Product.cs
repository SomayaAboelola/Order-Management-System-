

namespace Orders.Core.Entities
{
    public class Product :BaseEntity
    {
        //Product: ProductId, Name, Price, Stock

        public string Name { get; set; } = default!;
        public int Stock { get; set; }  
        public decimal Price { get; set; }
    }
}
