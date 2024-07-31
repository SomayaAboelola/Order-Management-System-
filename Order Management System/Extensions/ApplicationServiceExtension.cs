using Order_Management_System.Helper;
using Orders.Repository;
using Orders.Repository.BasketRepository;
using Orders.Services;

namespace Order_Management_System.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationService (this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenServices ,TokenServices>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICustomerServices, CustomerServices>();
            services.AddScoped<IProductServices, ProductServices>();

            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IOrderValidationServices, OrderValidationServices>();
            services.AddScoped<EmailService>();
            services.AddAutoMapper(typeof(MappingProfile));

            return services;    
        }
    }
}
