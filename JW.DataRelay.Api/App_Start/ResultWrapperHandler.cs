using JW.DataRelay.Api.App_Start.Filter;
using JW.DataRelay.Api.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace JW.DataRelay.Api.App_Start
{
    public class ResultWrapperHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = await base.SendAsync(request, cancellationToken);
            WrapResultIfNeeded(request, result);
            return result;
        }

        protected virtual void WrapResultIfNeeded(HttpRequestMessage request, HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            response.Headers.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true,
                MaxAge = TimeSpan.Zero,
                MustRevalidate = true
            };
            var actionDescriptor = request.GetActionDescriptor();
            if (actionDescriptor != null)
            {
                var wrapAttr = actionDescriptor.GetCustomAttributes<WrapResultAttribute>().FirstOrDefault();
                if (wrapAttr != null)
                {
                    if (!wrapAttr.WrapOnSuccess)
                    {
                        return;
                    }
                    if (!wrapAttr.WrapOnError)
                    {
                        return;
                    }
                }
            }

            object resultObject;
            if (!response.TryGetContentValue(out resultObject) || resultObject == null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ObjectContent<ActionResult>(
                    new ActionResult(),
                    GlobalConfiguration.Configuration.Formatters.JsonFormatter
                    );
                return;
            }

            if (resultObject is ActionResult)
            {
                return;
            }

            response.Content = new ObjectContent<ActionResult>(
                new ActionResult() { Data = resultObject },
                GlobalConfiguration.Configuration.Formatters.JsonFormatter
                );
        }
    }
}