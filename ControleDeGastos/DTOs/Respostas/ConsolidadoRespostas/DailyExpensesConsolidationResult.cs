namespace ControleDeGastos.DTOs.Respostas.ConsolidadoRespostas
{
    public class DailyExpensesConsolidationResult
    {
        public List<ObterGastosDiariosConsolidadosPorDiaResposta> ListaDeGastosPorDia { get; set; } = [];
        public decimal Total { get; set; }
    }
}
