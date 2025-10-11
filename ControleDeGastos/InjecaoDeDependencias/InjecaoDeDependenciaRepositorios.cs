using ControleDeGastos.Repositorios.ImplementacaoRepositorios;
using ControleDeGastos.Repositorios.InterfaceRepositorios;

namespace ControleDeGastos.InjecaoDeDependencias
{
    public static class InjecaoDeDependenciaRepositorios
    {
        public static void AdicionarInjecaoDeRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IControleDeGastosRepositorio, ControleDeGastosRepositorio>();
            services.AddScoped<IOperacoesGenericas, OperacoesGenericas>();
        }
    }
}
