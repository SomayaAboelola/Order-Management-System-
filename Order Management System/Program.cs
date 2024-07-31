
using Order_Management_System.Extensions;
using Order_Management_System.Helper;
using StackExchange.Redis;

namespace Order_Management_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddDbContext<OrderDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<IdentityDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"), b => b.MigrationsAssembly("Orders.Repository"));
            });


            builder.Services.AddSingleton<IConnectionMultiplexer>(Services =>
            {
                var Connect = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(Connect);
            });

            builder.Services.AddIdentityService();
            builder.Services.AddAuthenticationService(builder.Configuration);

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", Roles =>
                    Roles.RequireRole("Admin")); 
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerService();
            var app = builder.Build();

            await ApplySeeding.SeedAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                SwaggerServiceExtension.UseSwaggerMiddelware(app);
            }
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
