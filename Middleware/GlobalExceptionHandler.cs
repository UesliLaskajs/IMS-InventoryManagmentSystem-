

namespace IMS_InventoryManagmentSystem_.Middleware
{
    public class GlobalExceptionHandler
    {
        public readonly RequestDelegate _next;
        public readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next= next;
            _logger= logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An Unhandled Error Ocurred.");

                await HandleExceptionAsync(httpContext,e);
            }

        }
        private async Task HandleExceptionAsync(HttpContext http, Exception ex)
        {
            http.Response.StatusCode = 500;
            http.Response.ContentType = "application/json";

            var errorResponse = new
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Source = ex.Source,
                InnerException = ex.InnerException?.Message,
                Path = http.Request.Path
            };

            await http.Response.WriteAsJsonAsync(errorResponse);
        }
    }

   
}
