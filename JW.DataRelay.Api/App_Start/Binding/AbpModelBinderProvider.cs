using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace JW.DataRelay.Api.App_Start.Binding
{
    public class AbpModelBinderProvider: ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return new AbpModelBinder();
        }
    }
}