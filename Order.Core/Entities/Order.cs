using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Core.Entities
{
   public class Order :BaseEntity
    {
        //OrderId, CustomerId, OrderDate, TotalAmount, OrderItems,PaymentMethod, Status

        public Customer Customer { get; set; } = default!;
        public int CustomerId { get; set; }
        
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus orderStatus { get; set; } = OrderStatus.Pending;

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public double TotalAmount { get; set; }

        public double? PriceWithDiscount { get; set; }
        public string? PaymentMethod { get; set; } =default!;
    }
}

