using ControleDeGastos.Servico.ImplementacaoServicos;
using ControleDeGastos.Servico.InterfaceServicos;

namespace ControleDeGastos.InjecaoDeDependencias
{
    public static class InjecaoDeDependenciaServices
    {
        public static void AdicionarInjecaoDeServicos(this IServiceCollection services)
        {
            services.AddScoped<IControleDeGastosServico, ControleDeGastosServico>();
        }
    }
}
