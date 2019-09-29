using Donate.Shared.API.Extensions;
using Donate.Shared.API.Request;
using Donate.Shared.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Donate.Shared.API.Filters
{
    public class RequestContextFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var requestContext = context.GetServiceProvider().GetServiceOrThrow<IRequestContext>();
            PopulateAsyncInformation(requestContext, context);
            PopulateRequestIdentifierInformation(requestContext, context);
            PopulateRequestIdentityInformation(requestContext, context);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        private static void PopulateRequestIdentityInformation(IRequestContext requestContext, ActionExecutingContext context)
        {
            requestContext.AssignRequestIdentityInformation("rushilojageer@gmail.com");
        }

        private static void PopulateRequestIdentifierInformation(IRequestContext requestContext, ActionContext context)
        {
            var requestId = context.HttpContext.GetHeaders().GetGuidOrDefault("RequestId");
            var parentRequestId = context.HttpContext.GetHeaders().GetGuidOrDefault("ParentRequestId");
            requestContext.AssignRequestIdentifiers(requestId, parentRequestId);
        }

        private static void PopulateAsyncInformation(IRequestContext requestContext, ActionContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("IsAsync", out var isAsync))
            {
                requestContext.IsAsync = false;
                return;
            }

            if (!bool.TryParse(isAsync.ToString(), out var isAsyncValue))
            {
                requestContext.IsAsync = false;
                return;
            }

            requestContext.IsAsync = isAsyncValue;
        }

    }
}
