using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using NLog;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.Enums;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Infrastructure.Asp.Filters
{
    public class MobileExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            switch (context.Exception)
            {
                case DomainException dx:
                {
                    var response = new ResponseModel
                    {
                        Message = dx.Message,
                        Success = false,
                        ErrorCode = dx.ErrorCode
                    };
                    context.Response =
                        context.Response =
                            context.Request.CreateResponse(HttpStatusCode.OK,
                                response);
                    return;
                }
                case Exception ex:
                {
                    var logger = LogManager.GetCurrentClassLogger();
                    logger.Error(ex);

                    var response = new ResponseModel
                    {
                        Message = "خطای داخلی سرور رخ داده است",
                        Success = false,
                        ErrorCode = ErrorCode.Default
                    };
                    context.Response =
                        context.Response =
                            context.Request.CreateResponse(HttpStatusCode.OK,
                                response);
                    break;
                }
            }
        }
    }
}