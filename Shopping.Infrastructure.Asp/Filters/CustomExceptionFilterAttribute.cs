using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using NLog;
using Shopping.Infrastructure.Core;

namespace Shopping.Infrastructure.Asp.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is DomainException dx)
            {
                context.Response =
                    context.Response =
                        context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                            dx.Message);
                return;
            }
            // ReSharper disable once UnusedVariable
            if (context.Exception is Exception ex)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);

                context.Response =
                    context.Response =
                        context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                            "خطای داخلی سرور");
            }
        }
    }
}