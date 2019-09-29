using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Donate.Shared.API.Extensions;
using Donate.Shared.API.Request;
using Donate.Shared.ResponseQueue;
using Donate.Shared.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.Shared.API.Filters
{
    public class AsynchronousActionExecutionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var requestContext = context.GetServiceProvider().GetServiceOrThrow<IRequestContext>();

            if (!requestContext.IsAsync)
            {
                await next();
                return;
            }

            ReturnAccepted(context);
            await ProcessRequestAsynchronously(context, requestContext);
        }

        private void ReturnAccepted(ActionExecutingContext context)
        {
            context.Result = new ContentResult
            {
                Content = "Processing Request In Background",
                StatusCode = (int) HttpStatusCode.OK
            };
        }

        private async Task ProcessRequestAsynchronously(ActionExecutingContext context, IRequestContext requestContext)
        {
            using (var scope = context.HttpContext.RequestServices.CreateScope())
            {
                var queue = scope.ServiceProvider.GetServiceOrThrow<IResponseQueue>();
                var newRequestContext = scope.ServiceProvider.GetServiceOrThrow<IRequestContext>();
                newRequestContext.Copy(requestContext);

                await ProcessRequestAsynchronously(context, queue, scope);
            }
        }

        private async Task ProcessRequestAsynchronously(ActionExecutingContext context, IResponseQueue queue, IServiceScope serviceScope)
        {
            try
            {
                var controller = serviceScope.ServiceProvider.GetService(context.Controller.GetType());
                if (controller == null) throw new Exception();
                var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                if (actionDescriptor == null) throw new Exception();
                var action = actionDescriptor.MethodInfo;
                if (action == null) throw new Exception();

                if (typeof(Task).IsAssignableFrom(action.ReturnType))
                {
                    var response = action.Invoke(controller, context.ActionArguments.Values.ToArray());
                    var responseTask = (Task) response;
                    var result = await GetResult(responseTask);
                    queue.PostOkResponse(Guid.NewGuid(), result);
                }
                else
                {
                    var response = action.Invoke(controller, context.ActionArguments.Values.ToArray());
                    queue.PostOkResponse(Guid.NewGuid(), response);
                }
            }
            catch (Exception)
            { 

            }
        }

        private static async Task<object> GetResult(Task task)
        {
            await task.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            return resultProperty.GetValue(task);
        }
    }
}