
namespace Orders.Core.Entities
{
    public class OrderItem :BaseEntity
    {
        // OrderItemId, OrderId, ProductId, Quantity, UnitPrice,Discount
        private OrderItem()
        {

        }

        public OrderItem(int productId, decimal price, int quantity /*,decimal discount*/)
        {
            ProductId = productId;
            UnitPrice = price;
            Quantity = quantity;
           // Discount = discount;    
        }
        public int OrderId { get; set; }    
        public Product Product { get; set; } = default!;
       public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }

    }
}
