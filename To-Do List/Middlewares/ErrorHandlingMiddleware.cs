
namespace To_Do_List.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                context.Response.Redirect("/Tasks/Error");
                _logger.LogError("\n \n CATCH EXCEPTION \n \n EXCEPTION INFO: \n \n " + ex.Message);
            }
        }
    }
}
