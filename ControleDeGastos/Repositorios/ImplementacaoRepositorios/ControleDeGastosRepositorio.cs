using ControleDeGastos.Data.Contexto;
using ControleDeGastos.Data.ResultadoPaginado.Extensoes;
using ControleDeGastos.DTOs.Requisicao.GastosDiarios;
using ControleDeGastos.DTOs.Requisicoes.GastosFixosRequisicoes;
using ControleDeGastos.Modelos;
using ControleDeGastos.Queries;
using ControleDeGastos.Repositorios.InterfaceRepositorios;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Repositorios.ImplementacaoRepositorios
{
    public class ControleDeGastosRepositorio(AppDbContext context) : IControleDeGastosRepositorio
    {
        #region GastosDiarios
        public IQueryable<GastosDiarios> ObterGastosDiariosBase(ObterGastosDiariosRequisicao requisicao)
        {
            return context.gastos_diarios
            .FiltrarRemoverDeletados()
            .FiltrarPorCategorias(requisicao.Categoria)
            .FiltrarPorPeriodoDeLancamento(requisicao.InicioDoPeriodo, requisicao.FimDoPeriodo)
            .Include(x => x.categoria)
            .OrderBy(x => x.DataDoLancamento)
            .ThenBy(x => x.IdGastosDiarios);
        }
        public async Task<(List<GastosDiarios> itens, int totalItens)> ObterGastosDiarios(ObterGastosDiariosRequisicao requisicao)
        {
            return await ObterGastosDiariosBase(requisicao).PaginarAsync(requisicao.Pagina, requisicao.QtdPorPagina);
        }
        public async Task<GastosDiarios?> ObterGastoDiarioPorId(int id)
        {
            return await context.gastos_diarios.FindAsync(id);
        }
        #endregion

        #region CategoriasDeGastos
        public IQueryable<CategoriasDeLancamentos> ObterCategoriasDeLancamentosBase()
        {
            return context.categorias_de_lancamentos.FiltrarRemoverDeletados();
        }
        public async Task<List<CategoriasDeLancamentos>> ObterCategoriasDeLancamentos()
        {
            return await ObterCategoriasDeLancamentosBase().ToListAsync();
        }
        public async Task<CategoriasDeLancamentos?> ObterCategoriasDeLancamentosPorId(int id)
        {
            return await context.categorias_de_lancamentos.FindAsync(id);
        }
        #endregion

        #region GastosFixos
        public IQueryable<GastosFixos> ObterGastosFixosBase(ObterGastosFixosRequisicao requisicao)
        {
            return context.gastos_fixos
            .FiltrarRemoverDeletados()
            .FiltrarPorDescricao(requisicao.DescricaoDoGasto)
            .FiltrarPorPeriodo(requisicao.InicioDoPeriodo, requisicao.FimDoPeriodo)
            .OrderBy(x => x.DataDoLancamento)
            .ThenBy(x => x.IdGastosFixos);
        }
        public async Task<(List<GastosFixos> itens, int totalItens)> ObterGastosFixos(ObterGastosFixosRequisicao requisicao)
        {
            return await ObterGastosFixosBase(requisicao).PaginarAsync(requisicao.Pagina, requisicao.QtdPorPagina);
        }
        public async Task<GastosFixos?> ObterGastosFixosPorId(int id)
        {
            return await context.gastos_fixos.FindAsync(id);
        }
        #endregion
    }
}
