using ControleDeGastos.Data.Contexto;
using ControleDeGastos.Data.ResultadoPaginado.Extensoes;
using ControleDeGastos.DTOs.Requisicao;
using ControleDeGastos.Models;
using ControleDeGastos.Repositorios.InterfaceRepositorios;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Repositorios.ImplementacaoRepositorios
{
    public class ControleDeGastosRepositorio(AppDbContext context) : IControleDeGastosRepositorio
    {
        public async Task<(List<GastosDiarios> itens, int totalItens)> ObterGastosDiarios(ObterGastosDiarios obterGastosDiarios)
        {
            return await context.gastos_diarios
            .Include(x => x.categoria)
            .OrderBy(x => x.DataDoLancamento)
            .ThenBy(x => x.IdGastos)
            .PaginarAsync(obterGastosDiarios.Pagina, obterGastosDiarios.QtdPorPagina);
        }

        public async Task<List<CategoriasDeLancamentos>> ObterCategoriasDeLancamentos()
        {
            return await context.categorias_de_lancamentos.ToListAsync();
        }
    }
}
