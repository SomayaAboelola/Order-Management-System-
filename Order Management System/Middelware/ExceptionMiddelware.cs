using Order_Management_System.Error;
using System.Net;
using System.Text.Json;

namespace Order_Management_System.Middelware
{
    public class ExceptionMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IWebHostEnvironment _environment;

        public ExceptionMiddelware(RequestDelegate next, ILoggerFactory loggerFactory, IWebHostEnvironment environment)
        {
            _next = next;
            _loggerFactory = loggerFactory;
            _environment = environment;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {

                await _next.Invoke(httpContext);

            }
            catch (Exception ex)
            {
                var logger = _loggerFactory.CreateLogger<ExceptionMiddelware>();
                logger.LogError(ex.Message);

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var Response = _environment.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                                                            : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var jsonResponse = JsonSerializer.Serialize(Response, options);

                httpContext.Response.WriteAsync(jsonResponse);

            }
        }
    }
}
