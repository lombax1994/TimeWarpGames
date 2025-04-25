using System;
using System.Globalization;
using System.Web.Mvc;

public class FlexibleDecimalModelBinder : IModelBinder
{
    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
        var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueResult == null || string.IsNullOrWhiteSpace(valueResult.AttemptedValue))
            return null;

        var attempted = valueResult.AttemptedValue.Replace(",", ".");

        if (decimal.TryParse(attempted, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
        {
            return result;
        }

        bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Ongeldig getalformaat.");
        return null;
    }
}