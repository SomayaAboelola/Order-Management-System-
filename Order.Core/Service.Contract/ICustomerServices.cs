using Order_Management_System.Dtos;
using System;


namespace Orders.Core.Service.Contract
{
    public interface ICustomerServices
    {
        //Create a new customer
        Task<CustomerDto> CreateCustomerAsync(CustomerDto  input);
       // Get all orders for a customer
        Task<IEnumerable<OrderResultDto>>GetCustomerOrderAsync(int  customerId);
    }
}
