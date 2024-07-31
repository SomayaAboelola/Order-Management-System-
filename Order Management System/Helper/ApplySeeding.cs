

using Orders.Repository._identity;

namespace Order_Management_System.Helper
{
    public  static class ApplySeeding
    {
        public static async Task SeedAsync(WebApplication app)
        {
            using var Scope=app.Services.CreateScope();

            var Services=Scope.ServiceProvider;

            var _context = Services.GetRequiredService<OrderDbContext>();

            var _contextIdentity = Services.GetRequiredService<IdentityDbContext>();

            var _contextSeed = Services.GetRequiredService<UserManager<IdentityUser>>();

            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();


            try
            {
                await _context.Database.MigrateAsync();

                await _contextIdentity.Database.MigrateAsync();
                await UserDbContextSeed.SeedUserASync(_contextSeed);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex.Message);
            }

        }
    }
}
