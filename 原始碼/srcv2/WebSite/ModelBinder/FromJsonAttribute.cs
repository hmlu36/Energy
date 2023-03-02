using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace JamZoo.Project.WebSite.ModelBinder
{
    public class FromJsonAttribute : CustomModelBinderAttribute
    {
        private readonly static JavaScriptSerializer ser = new JavaScriptSerializer();
        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }

        private class JsonModelBinder : IModelBinder
        {

            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                var s = controllerContext.HttpContext.Request[bindingContext.ModelName];
                if (string.IsNullOrEmpty(s))
                    return null;
                return ser.Deserialize(s, bindingContext.ModelType);
            }
        }
    }
}