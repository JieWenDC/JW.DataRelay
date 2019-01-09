using JW.DataRelay.Api.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace JW.DataRelay.Api.App_Start.Filter
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            var result = new ActionResult()
            {
                Status = false,
                Error = new ErrorInfo()
                {
                    Message = actionExecutedContext.Exception.Message,
                    Details = actionExecutedContext.Exception.ToString(),
                }
            };
            if (actionExecutedContext.Exception is System.Web.Http.HttpResponseException)
            {
                var ex = actionExecutedContext.Exception as System.Web.Http.HttpResponseException;
                result.Error.Details = ex.ToString();
                result.Error.Message = ex.Message;
                statusCode = ex.Response.StatusCode;
            }

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
              statusCode, result);
        }
    }
}