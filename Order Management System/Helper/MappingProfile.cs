

namespace Order_Management_System.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDto , Customer>().ReverseMap();
            CreateMap<Order ,OrderDto>().ReverseMap(); 
            CreateMap<Order ,OrderResultDto>().ReverseMap();
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<OrderItem ,OrderItemDto>().ReverseMap(); 
            CreateMap<IdentityUser ,UserDto>().ReverseMap(); 
            

        }
    }
}
