using ExpensesControl.Data.ResultPattern.Base;
using Microsoft.AspNetCore.Mvc;
namespace ExpensesControl.Data.ResultPattern.Extensions;

public static class ResultPatternExtension
{
    public static IActionResult ToIActionResult<T>(this ResultPattern<T> result, ControllerBase controller)
    {
        if (result.IsSuccess)
            return controller.Ok(result.Value);

        var problem = new ProblemDetails
        {
            Status = result.StatusCode,
            Title = result.Title,
            Detail = result.Detail,
            Instance = controller.HttpContext.Request.Path
        };

        return controller.StatusCode(problem.Status.Value, problem);
    }
}
