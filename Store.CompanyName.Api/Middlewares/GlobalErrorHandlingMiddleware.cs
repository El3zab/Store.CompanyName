using Domain.Exceptions;
using Shared.ErrorModels;
using System.Threading.Tasks;

namespace Store.CompanyName.Api.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger) // Must End With Middleware
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                // log Exception
                _logger.LogError(ex, ex.Message);

                // 1. Set Status Code For Responce 
                // 2. Set Content Type For Responce
                // 3. Responce Object (Body)

                // 4. Return Responce

                //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ErrorDetails()
                {
                   ErrorMessage = ex.Message
                };

                response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = response.StatusCode;


                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
