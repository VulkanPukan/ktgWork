using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StrengthTracker2.Web.Helpers
{
    public class BooleanModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            //var result = false;
            //bool.TryParse((string)value.ConvertTo(bool), out result);
            return value.RawValue;
        }
    }
}