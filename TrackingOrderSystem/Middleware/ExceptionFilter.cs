using Newtonsoft.Json;
using TrackingOrderSystem.Exceptios;

namespace TrackingOrderSystem.Middleware
{
    /// <summary>
    /// Middleware return output: { message, statusCode }
    /// </summary>
    public class ExceptionFilter
    {
        private readonly RequestDelegate _next;

        public ExceptionFilter(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ExceptionHttp httpEx)
            {
                httpContext.Response.StatusCode = httpEx.StatusCode;
                httpContext.Response.ContentType = "application/json";

                var result = JsonConvert.SerializeObject(new
                {
                    message = httpEx.Message,
                    statusCode = httpEx.StatusCode,
                });
                await httpContext.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = 500; 
                httpContext.Response.ContentType = "application/json";

                var result = JsonConvert.SerializeObject(new
                {
                    message = ex.Message,
                    statusCode = 500
                });
                await httpContext.Response.WriteAsync(result);
            }
        }
    }
}