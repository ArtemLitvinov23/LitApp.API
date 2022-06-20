using LitChat.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace LitChat.API.Filters
{
    public class ValidatorActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(key => key.Key, key => key.Value.Errors.Select(x => x.ErrorMessage))
                    .ToArray();

                var response = new ValidationErrorResponse();

                foreach (var error in errors)
                {
                    foreach(var subError in error.Value)
                    {
                        var errorModel = new ValidationError()
                        {
                            Field = error.Key,
                            Message = subError
                        };
                        response.ErrorResponse.Add(errorModel);
                    }
                }
                context.Result = new BadRequestObjectResult(response);
                return;
            }

            await next();
        }
    }
}
