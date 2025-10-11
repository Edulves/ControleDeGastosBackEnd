using ControleDeGastos.DTOs.Requisicao;
using ControleDeGastos.Models;

namespace ControleDeGastos.Repositorios.InterfaceRepositorios
{
    public interface IControleDeGastosRepositorio
    {
        Task<(List<GastosDiarios> itens, int totalItens)> ObterGastosDiarios(ObterGastosDiarios obterGastosDiarios);
        Task<List<CategoriasDeLancamentos>> ObterCategoriasDeLancamentos();
    }
}
