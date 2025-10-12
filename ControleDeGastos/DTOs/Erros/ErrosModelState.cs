using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControleDeGastos.DTOs.Erros
{
    public class ErrosModelState : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var erros = context.ModelState
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => $"{e.ErrorMessage}").ToArray()
                    );

                // Transformar em uma única string
                string errosComoString = string.Join("; ", erros.Select(kvp =>
                {
                    var titulo = kvp.Key; // ou kvp.Value.FirstOrDefault() se quiser o primeiro erro como título
                    var detalhes = string.Join(", ", kvp.Value);
                    return $"{titulo}: {detalhes}";
                }));

                context.Result = new BadRequestObjectResult(new DetalhesDeProblemas
                {
                    Status = 400,
                    Titulo = "Um ou mais erros de validação",
                    Detalhe = errosComoString,
                    Instancia = context.HttpContext.Request.Path,
                });
            }
        }
    }
}
