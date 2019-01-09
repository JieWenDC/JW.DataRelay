using JW.DataRelay.Api.App_Start;
using JW.DataRelay.Api.App_Start.Binding;
using JW.DataRelay.Api.App_Start.Filter;
using JW.DataRelay.Framework.Json;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace JW.DataRelay.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new AjaxJsonResolver();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //替换默认的：检索与参数相关联的动作绑定的方法值
            //GlobalConfiguration.Configuration.Services.Insert(typeof(System.Web.Http.ModelBinding.ModelBinderProvider), 0, new AbpModelBinderProvider());
            //注册过滤器
            GlobalConfiguration.Configuration.Filters.Add(new AuthorizationFilter());
            GlobalConfiguration.Configuration.Filters.Add(new ApiExceptionFilterAttribute());
            //注册消息处理器
            GlobalConfiguration.Configuration.MessageHandlers.Add(new ResultWrapperHandler());
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
