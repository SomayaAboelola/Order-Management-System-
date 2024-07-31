using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Core.Dtos
{
    public class OrderResultDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus orderStatus { get; set; } = OrderStatus.Pending;

        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();

        public double TotalAmount { get; set; }
        public double? PriceWithDiscount { get; set; }
    }
}
