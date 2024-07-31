using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Core.Entities
{
    public class Invoice : BaseEntity
    {
        //Invoice: InvoiceId, OrderId, InvoiceDate, TotalAmount
        public Order Order { get; set; } = default!;
        public int OrderId { get; set; }
        public DateTimeOffset InvoiceDate { get; set; } = DateTimeOffset.UtcNow;

        public double TotalAmount { get; set; }


    }
}
