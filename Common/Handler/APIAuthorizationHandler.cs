using Common.Helper;
using Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Handler
{
    public class APIAuthorizationHandler : AuthorizationHandler<APIAuthorization>
    {
        public const string API_KEY_HEADER_NAME = "API-KEY";
        public const string AUHTORIZE_HEADER_NAME = "Authorization";
        private readonly IHttpContextAccessor httpContextAccessor;

        public APIAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, APIAuthorization requirement)
        {
            SucceedRequirementIfApiKeyPresentAndValid(context, requirement);
            return Task.CompletedTask;
        }

        private void SucceedRequirementIfApiKeyPresentAndValid(AuthorizationHandlerContext context, APIAuthorization requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                var routeData = httpContext.Request;
                var apiKey = routeData.Headers[API_KEY_HEADER_NAME].FirstOrDefault();
                var header = routeData.Headers[AUHTORIZE_HEADER_NAME].FirstOrDefault();
                if (apiKey != null && requirement.ApiKeys.Any(requiredApiKey => apiKey == requiredApiKey))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    var unauthorized = new BaseResponse().Fail(null, MessageHelper.API_KEY_UNAUTHORIZED);

                    var filterContext = context.Resource as DefaultHttpContext;
                    if (filterContext != null)
                    {
                        var response = filterContext.Response;
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(unauthorized);
                        var message = Encoding.UTF8.GetBytes(json);

                        context.Fail();
                        response.OnStarting(() =>
                        {
                            filterContext.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        });
                        response.ContentType = "application/json";
                        _ = response.Body.WriteAsync(message, 0, message.Length);
                    }
                }
            }
        }
    }
}
