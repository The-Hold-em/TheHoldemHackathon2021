

using THH.Shared.Core.ExtensionMethods;

using Microsoft.AspNetCore.Mvc.Filters;

namespace THH.Shared.Core.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = context.GetBadRequestResultErrorDtoForModelState();
        }
    }
}
