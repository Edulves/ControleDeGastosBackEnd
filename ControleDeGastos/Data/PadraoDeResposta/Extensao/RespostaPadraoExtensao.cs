using ControleDeGastos.DTOs.Erros;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ControleDeGastos.Data.PadraoDeResposta.Base;

namespace ControleDeGastos.Data.PadraoDeResposta.Extensao
{
    public static class RespostaPadraoExtensao
    {
        /// <summary>
        /// Converte um Result<T> em IActionResult padronizado com ProblemDetails + campo "errors".
        /// </summary>
        public static IActionResult ToIActionResult<T>(this RespostaPadrao<T> result, ControllerBase controller)
        {
            if (!result.IsSuccess)
            {
                var problem = new DetalhesDeProblemas
                {
                    Status = result.StatusCode,
                    Titulo = result.Title,
                    Detalhe = string.Join("; ", result.ErrorMessages),
                    Instancia = controller.HttpContext.Request.Path
                };

                return controller.StatusCode(problem.Status.Value, problem);
            }

            return controller.Ok(result.Value);
        }

        /// <summary>
        /// Converte um Result&lt;(byte[] fileContents, string fileName)&gt; em IActionResult File(...)
        /// ou, em caso de falha, em ProblemDetails padronizado.
        /// </summary>
        public static IActionResult ToFileExcelResult(this RespostaPadrao<(byte[] fileContents, string fileName)> result, ControllerBase controller,
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            if (!result.IsSuccess)
            {
                var converted = RespostaPadrao<object>.Failure(
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
