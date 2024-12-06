using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Server.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            context.Result = new ObjectResult(new
            {
                message = "An unexpected error occurred.",
                details = context.Exception.Message
            })
            {
                StatusCode = 500
            };
        }
    }
}
