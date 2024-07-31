using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Core.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTimeOffset InvoiceDate { get; set; } = DateTimeOffset.UtcNow;

        public double TotalAmount { get; set; }
    }
}
