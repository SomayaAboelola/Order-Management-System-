using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Orders.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(IdentityUser user, UserManager<IdentityUser> userManager)
        {
            // Private Claims (User_Defined)

            var authClaim = new List<Claim>()
            {
                new Claim (ClaimTypes.Name ,user.UserName) ,
                new Claim(ClaimTypes.Email ,user.Email)
            };

            var userRole = await userManager.GetRolesAsync(user);
            foreach (var Role in userRole)
            {
                authClaim.Add(new Claim(ClaimTypes.Role, Role));
            }

            // Security Key

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:AuhKey"]));

            // Register Claim 

            var token = new JwtSecurityToken(

                audience: _configuration["jwt:AuthAudiance"],
                issuer: _configuration["jwt:AuthIssue"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["jwt:AuthExpire"] ?? "0")),
                claims: authClaim,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.Aes128CbcHmacSha256)

                    );

            // value 
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
