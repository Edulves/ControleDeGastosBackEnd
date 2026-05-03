namespace ControleDeGastos.DTOs.Respostas.ConsolidadoRespostas
{
    public class DailyExpensesPerCategoryResult
    {
        public List<ObterGastosDiariosConsolidadosPorCategoriasResposta> ListaDeGastosPorCategoria { get; set; } = new List<ObterGastosDiariosConsolidadosPorCategoriasResposta>();
        public decimal TotalDeGastos { get; set; }
    }
}
