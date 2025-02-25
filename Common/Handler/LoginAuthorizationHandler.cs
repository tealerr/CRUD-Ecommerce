using Common.Helper;
using Common.Repositories;
using Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Common.Handler
{
    public class LoginAuthorizationHandler : AuthorizationHandler<LoginAuthorization>
    {
        public const string API_KEY_HEADER_NAME = "API-KEY";
        public const string AUHTORIZE_HEADER_NAME = "Authorization";

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LoginAuthorization requirement)
        {
            SucceedRequirementIfApiKeyPresentAndValid(context, requirement);
            return Task.CompletedTask;
        }

        private void SucceedRequirementIfApiKeyPresentAndValid(AuthorizationHandlerContext context, LoginAuthorization requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                var isError = false;
                var routeData = httpContext.Request;
                var apiKey = routeData.Headers[API_KEY_HEADER_NAME].FirstOrDefault();
                var header = routeData.Headers[AUHTORIZE_HEADER_NAME].FirstOrDefault();
                if (apiKey != null && requirement.ApiKeys.Any(requiredApiKey => apiKey == requiredApiKey))
                {
                    if (header != null)
                    {
                        try
                        {
                            var token = header.Substring("Bearer ".Length);
                            if (token != null)
                            {
                                var result = new UserRepositories().ValidateToken(token);
                                if (result)
                                {
                                    var filterContext = context.Resource as DefaultHttpContext;
                                    filterContext?.Response.HttpContext.Items.Add(CodeHelper.TOKEN_KEY, token);
                                    context.Succeed(requirement);
                                }
                                else
                                {
                                    isError = true;
                                }
                            }
                            else
                            {
                                isError = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            isError = true;
                        }
                    }
                    else
                    {
                        isError = true;
                    }
                }
                else
                {
                    isError = true;
                }

                if (isError)
                {
                    var unauthorized = new BaseResponse().Fail(null, "ขออภัย Token หมดอายุหรือไม่ถูกต้อง");

                    var filterContext = context.Resource as DefaultHttpContext;
                    var response = filterContext?.Response;

                    if (response != null)
                    {
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(unauthorized);
                        var message = Encoding.UTF8.GetBytes(json);

                        context.Fail();
                        response.OnStarting(() =>
                        {
                            response.StatusCode = 401;
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
