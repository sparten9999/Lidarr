using Nancy;
using Nancy.ErrorHandling;
using Lidarr.Http.Extensions;
using Nancy.Configuration;

namespace Lidarr.Http.ErrorManagement
{
    public class ErrorHandler : IStatusCodeHandler
    {
        private readonly INancyEnvironment _environment;

        public ErrorHandler(INancyEnvironment environment)
        {
            _environment = environment;
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return true;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            if (statusCode == HttpStatusCode.SeeOther || statusCode == HttpStatusCode.OK)
                return;

            if (statusCode == HttpStatusCode.Continue)
            {
                context.Response = new Response { StatusCode = statusCode };
                return;
            }

            if (statusCode == HttpStatusCode.Unauthorized)
                return;

            if (context.Response.ContentType == "text/html" || context.Response.ContentType == "text/plain")
                context.Response = new ErrorModel
                    {
                            Message = statusCode.ToString()
                    }.AsResponse(_environment, statusCode);
        }
    }
}
