using JW.DataRelay.Framework;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace JW.DataRelay.Api.App_Start.Binding
{
    public class AbpModelBinder: IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value == null)
            {
                var valueProvider = (System.Web.Http.ValueProviders.Providers.NameValuePairsValueProvider)bindingContext.ValueProvider;
            }
            bindingContext.Model = ReflectionHelper.ChangeType(value, bindingContext.ModelType);
            return true;
        }
    }
}