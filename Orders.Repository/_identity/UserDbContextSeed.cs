using Microsoft.AspNetCore.Identity;


namespace Orders.Repository._identity
{
    public static class UserDbContextSeed
    {
        public static async Task SeedUserASync(UserManager<IdentityUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var User = new IdentityUser()
                {
                    Email = "SomayMagdy290@gmail.com",
                    UserName = "SomayaMagdy",
                 
                };
                await userManager.CreateAsync(User, "Pa$sw0rd"); 
            }
        }   
    }
}
