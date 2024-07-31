using Microsoft.AspNetCore.Identity;


namespace Orders.Core.Service.Contract
{
    public interface ITokenServices
    {
        Task<string> CreateTokenAsync(IdentityUser user, UserManager<IdentityUser> userManager);

    }
}
