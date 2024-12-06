using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Server.ViewModels.Response;

namespace Server.Filters
{
    public class ResponseWrappingFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // No hacer nada antes de ejecutar la acción
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var response = new ApiResponse<object>
                {
                    Success = objectResult.StatusCode >= 200 && objectResult.StatusCode < 300,
                    Data = objectResult.Value ?? string.Empty ,
                    ErrorMessage = null
                };

                if (objectResult.StatusCode == 404)
                {
                    response.Success = false;
                    response.ErrorMessage = "Resource not found";
                }

                context.Result = new OkObjectResult(response);
            }
        }
    }
}
