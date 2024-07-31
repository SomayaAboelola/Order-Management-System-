using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Order_Management_System.Extensions
{
    public static class IdentityServicesExtension
    {

        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddApplicationService();

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<IdentityDbContext>();

            return services;
        }


        public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configu)
        {
            services.AddAuthentication/*(JwtBearerDefaults.AuthenticationScheme)*/
             (option =>
             {
                 option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             })
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = configu["jwt:AuthAudiance"],

                    ValidateIssuer = true,
                    ValidIssuer = configu["jwt:AuthIssue"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configu["jwt:AuhKey"]))


                };
            });
            return services;

        }
    }
}