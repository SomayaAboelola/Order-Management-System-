using Microsoft.OpenApi.Models;

namespace Order_Management_System.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "TalabatAPI", Version = "v1" });

                var SecurityyScheme = new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. Example: Authorization: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }

                };
                option.AddSecurityDefinition("bearer", SecurityyScheme);
                var SecurityRequierment = new OpenApiSecurityRequirement
                {
                    {SecurityyScheme ,new [] {"bearer"} }
                };
                option.AddSecurityRequirement(SecurityRequierment);

                option.CustomSchemaIds(type => type.ToString());
            });

            return services;
        }
        public static WebApplication UseSwaggerMiddelware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }

    }
}
