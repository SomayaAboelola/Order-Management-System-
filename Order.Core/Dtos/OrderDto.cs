namespace Orders.Core.Dtos
{

    public class OrderDto
    {
        public int Id { get; set; } 
        public int CustomerId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
      
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();

       //public PaymentInfo PaymentInfo { get; set; } = default!;
    }
}
