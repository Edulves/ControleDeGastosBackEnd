using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static ControleDeGastos.Data.PadraoDeResposta.Base.RespostaPadrao;

namespace ControleDeGastos.Data.PadraoDeResposta.Extensao
{
    public static class RespostaPadraoExtensao
    {
        /// <summary>
        /// Converte um Result<T> em IActionResult padronizado com ProblemDetails + campo "errors".
        /// </summary>
        public static IActionResult ToIActionResult<T>(this Result<T> result, ControllerBase controller)
        {
            if (!result.IsSuccess)
            {
                var problem = new ProblemDetails
                {
                    Status = result.StatusCode,
                    Title = result.Title,
                    Detail = string.Join("; ", result.ErrorMessages),
                    Instance = controller.HttpContext.Request.Path
                };

                // traceId
                problem.Extensions["traceId"] =
                    Activity.Current?.Id ?? controller.HttpContext.TraceIdentifier;


                var errors = new Dictionary<string, string[]>
                {
                    ["mensagemErro"] = result.ErrorMessages.ToArray()
                };
                problem.Extensions["errors"] = errors;

                return controller.StatusCode(problem.Status.Value, problem);
            }

            return controller.Ok(result.Value);
        }

        /// <summary>
        /// Converte um Result&lt;(byte[] fileContents, string fileName)&gt; em IActionResult File(...)
        /// ou, em caso de falha, em ProblemDetails padronizado.
        /// </summary>
        public static IActionResult ToFileExcelResult(this Result<(byte[] fileContents, string fileName)> result, ControllerBase controller,
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            if (!result.IsSuccess)
            {
                var converted = Result<object>.Failure(
                    errorMessage: result.ErrorMessages.First(),
                    statusCode: result.StatusCode ?? StatusCodes.Status400BadRequest,
                    title: result.Title,
                    otherErrorMessages: result.ErrorMessages.Skip(1).ToArray()
                );
                return converted.ToIActionResult(controller);
            }

            var (fileContents, fileName) = result.Value;
            return controller.File(fileContents, contentType, fileName);
        }
    }
}
