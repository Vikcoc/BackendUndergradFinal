using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace BackendUndergradFinal.Controllers
{
    public class ControllerWithError : Controller
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!ModelState.IsValid)
            {
                context.Result = BadRequest(
                    ModelState
                        .Values
                        .SelectMany(x => x.Errors
                            .Select(y => y.ErrorMessage))
                        .ToList()
                );
            }
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
