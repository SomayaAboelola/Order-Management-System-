using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Order_Management_System.Dtos;
using Orders.Core.Dtos;
using Orders.Core.Entities;
using Orders.Core.Repository.Contract;
using Orders.Repository._Data;

namespace Orders.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly OrderDbContext _context;
        public CustomerServices(OrderDbContext context,
                                 IUnitOfWork unitOfWork,
                                 IMapper mapper
                            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        public async Task<CustomerDto> CreateCustomerAsync(CustomerDto input)
        {
            var customer = new Customer();

            customer.Name = input.Name;
            customer.Email = input.Email;

            var mapCustomer = _mapper.Map<Customer>(customer);
            
            await _unitOfWork.Repository<Customer>().AddAsync(mapCustomer);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CustomerDto>(customer);

        }

        public async Task<IEnumerable<OrderResultDto>> GetCustomerOrderAsync(int customerId)
        {

           var orders= await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
            return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
        }
    }
}
