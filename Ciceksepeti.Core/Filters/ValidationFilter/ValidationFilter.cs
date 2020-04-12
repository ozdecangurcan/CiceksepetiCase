using Ciceksepeti.Dto.ApiResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ciceksepeti.Core.Filters.ValidationFilter
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                List<ApiResponse> responseList = new List<ApiResponse>();

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        var response = new ApiResponse
                        {
                            ResultCode = HttpStatusCode.BadRequest,
                            Data=error.Key,
                            Message = subError,
                            IsSuccess=false
                        };

                        responseList.Add(response);
                    }
                }

                context.Result = new BadRequestObjectResult(responseList);
                return;
            }

            await next();
        }
    }
}
