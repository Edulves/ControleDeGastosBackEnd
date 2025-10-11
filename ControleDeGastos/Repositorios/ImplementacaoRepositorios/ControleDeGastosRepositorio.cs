using ControleDeGastos.Data.Contexto;
using ControleDeGastos.Data.ResultadoPaginado.Extensoes;
using ControleDeGastos.DTOs.Requisicao;
using ControleDeGastos.DTOs.Resposta;
using ControleDeGastos.Models;
using ControleDeGastos.Repositorios.InterfaceRepositorios;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Repositorios.ImplementacaoRepositorios
{
    public class ControleDeGastosRepositorio(AppDbContext context) : IControleDeGastosRepositorio
    {
        #region GastosDiarios
        public async Task<(List<GastosDiarios> itens, int totalItens)> ObterGastosDiarios(ObterGastosDiariosRequisicao obterGastosDiarios)
        {
            return await context.gastos_diarios
            .Include(x => x.categoria)
            .OrderBy(x => x.DataDoLancamento)
            .ThenBy(x => x.IdGastos)
            .PaginarAsync(obterGastosDiarios.Pagina, obterGastosDiarios.QtdPorPagina);
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
