using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Core.Entities
{
    public class Customer  :BaseEntity
    {
        //CustomerId, Name, Email, Orders
     
      
        public string Name { get; set; } = default!;
        public string Email { get; set; } =default!;
        public ICollection<Order> Items { get; set; } = new HashSet<Order>();
        public Customer() { }  // Add a parameterless constructor

        public Customer(string name, string email)  // Existing constructor
        {
            name=Name;
            email=Email;
        }

    }
}
