using JW.DataRelay.Api.App_Start.Attribute;
using JW.DataRelay.Api.Models;
using JW.DataRelay.Business;
using JW.DataRelay.Framework.Exceptions;
using JW.DataRelay.Framework.Logging;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JW.DataRelay.Api.App_Start.Filter
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public bool AllowMultiple => true;

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            //判断是否继承了特性AllowAnonymousAttribute，继承则不做权限校验
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() || actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return await continuation();
            }
            if (actionContext.ActionDescriptor is ReflectedHttpActionDescriptor)
            {
                var methodInfo = ((ReflectedHttpActionDescriptor)actionContext.ActionDescriptor).MethodInfo;
                if (methodInfo == null)
                {
                    return await continuation();
                }
            }
            try
            {
                var Request = System.Web.HttpContext.Current.Request;
                var appkey = Request.Headers["appKey"];
                if (string.IsNullOrEmpty(appkey))
                {
                    throw new AuthorizationException(401001, "缺少参数:appKey");
                }
                var token = Request.Headers["token"];
                if (string.IsNullOrEmpty(token))
                {
                    throw new AuthorizationException(401002, "缺少参数:token");
                }
                var collectionName = Request.Headers["collectionName"];
                if (string.IsNullOrEmpty(collectionName))
                {
                    throw new AuthorizationException(401003, "缺少参数:collectionName");
                }
                var requestTimeStr = Request.Headers["requestTime"];
                if (string.IsNullOrEmpty(requestTimeStr))
                {
                    throw new AuthorizationException(401008, "缺少参数:requestTime");
                }
                var agent = new AgentBusiness().GetByAppKey(appkey);
                if (agent == null)
                {
                    throw new AuthorizationException(401004, "不存在的代理信息");
                }
                var requestTime = requestTimeStr.ToDateTime();
                if (requestTime == default(DateTime))
                {
                    throw new AuthorizationException(401009, "无效的参数：requestTime");
                }
                string param_str = actionContext.Request.Content.ReadAsStringAsync().Result;
                var str = string.Format("{0}{1}", param_str, agent.MasterSecret);
                str = str.MD5();
                if (str != token)
                {
                    throw new AuthorizationException(401007, "无效的签名");
                }
                var collectionRights = new DataCollectionRightBusiness().GetListByAgentIdAndCollectionName(agent.Id.ToString(), collectionName);
                if (!collectionRights.ExistsData())
                {
                    throw new AuthorizationException(401005, "未分配任何权限");
                }
                var actionAuthTypeConfig = actionContext.ActionDescriptor.GetCustomAttributes<AuthTypeAttribute>().FirstOrDefault();
                if (actionAuthTypeConfig == null)
                {
                    throw new AuthorizationException(401006, $"{actionContext.ActionDescriptor.ActionName}缺少权限配置");
                }
                var authType = actionAuthTypeConfig.ActionAuthType;
                if (collectionRights.Any(row => authType.HasFlag(row.Auth)))
                {
                    return await continuation();
                }
                else
                {
                    throw new AuthorizationException(401, "无权访问");
                }
            }
            catch (AuthorizationException ex)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new ObjectContent<ActionResult>(new ActionResult()
                    {
                        Status = false,
                        Error = new ErrorInfo()
                        {
                            Message = ex.Message,
                            Code = ex.HResult,
                        },
                    }, GlobalConfiguration.Configuration.Formatters.JsonFormatter)
                };
            }
        }
    }
}