using ControleDeGastos.Data.Contexto;
using ControleDeGastos.Data.ResultadoPaginado.Extensoes;
using ControleDeGastos.DTOs.Requisicao;
using ControleDeGastos.Models;
using ControleDeGastos.Queries;
using ControleDeGastos.Repositorios.InterfaceRepositorios;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Repositorios.ImplementacaoRepositorios
{
    public class ControleDeGastosRepositorio(AppDbContext context) : IControleDeGastosRepositorio
    {
        #region GastosDiarios
        public IQueryable<GastosDiarios> ObterGastosBase(ObterGastosDiariosRequisicao obterGastosDiarios)
        {
            return context.gastos_diarios
            .FiltraRemoverDeletados()
            .OrderBy(x => x.DataDoLancamento)
            .ThenBy(x => x.IdGastos);
        }

        public async Task<(List<GastosDiarios> itens, int totalItens)> ObterGastosDiarios(ObterGastosDiariosRequisicao obterGastosDiarios)
        {
            return await ObterGastosBase(obterGastosDiarios).PaginarAsync(obterGastosDiarios.Pagina, obterGastosDiarios.QtdPorPagina);
        }

        public async Task<GastosDiarios?> ObterGastoDiarioPorId(int id)
        {
            return await context.gastos_diarios.FindAsync(id);
        }
        #endregion

        #region CategoriasDeGastos
        public async Task<List<CategoriasDeLancamentos>> ObterCategoriasDeLancamentos()
        {
            return await context.categorias_de_lancamentos.ToListAsync();
        }
        #endregion 
    }
}
