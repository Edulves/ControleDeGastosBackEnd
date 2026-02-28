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
            .FiltrarPorCategorias(requisicao.Categoria)
            .FiltrarPorPeriodoDeLancamento(requisicao.InicioDoPeriodo, requisicao.FimDoPeriodo)
            .FiltrarPorMeseAno(requisicao.Ano, requisicao.Mes)
            .FiltrarPorObservacao(requisicao.Observacao)
            .FiltrarRemoverDeletados()
            .Include(x => x.categoria)
            .OrderBy(x => x.DataDoLancamento)
            .ThenBy(x => x.IdGastosDiarios);
        }
        public async Task<decimal> ObterSomaGastosDiarios(ObterGastosDiariosRequisicao requisicao)
        {
            return await ObterGastosDiariosBase(requisicao).SumAsync(x => x.Valorgasto);
        }
        public async Task<List<GastosDiarios>> ObterGastosDiariosLista(ObterGastosDiariosRequisicao requisicao)
        {
            return await ObterGastosDiariosBase(requisicao).ToListAsync();
        }
        public async Task<(List<GastosDiarios> itens, int totalItens)> ObterGastosDiariosPaginado(ObterGastosDiariosRequisicao requisicao)
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
            return await ObterCategoriasDeLancamentosBase().OrderBy(x => x.NomeDaCategoria).ToListAsync();
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
            .FiltrarPorMeseAno(requisicao.Ano, requisicao.Mes)
            .FiltrarPorPeriodo(requisicao.InicioDoPeriodo, requisicao.FimDoPeriodo)
            .OrderBy(x => x.DataDoLancamento)
            .ThenBy(x => x.IdGastosFixos);
        }
        public async Task<List<GastosFixos>> ObterGastosFixosLista(ObterGastosFixosRequisicao requisicao)
        {
            return await ObterGastosFixosBase(requisicao).ToListAsync();
        }
        public async Task<decimal> ObterSomaGastosFixos(ObterGastosFixosRequisicao requisicao)
        {
            return await  ObterGastosFixosBase(requisicao).SumAsync(x => x.ValorGastoFixo);
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
