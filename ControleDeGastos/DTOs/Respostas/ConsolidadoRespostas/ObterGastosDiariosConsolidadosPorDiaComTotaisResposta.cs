namespace ControleDeGastos.DTOs.Respostas.ConsolidadoRespostas
{
    public class ObterGastosDiariosConsolidadosPorDiaComTotaisResposta
    {
        public List<ObterGastosDiariosConsolidadosPorDiaResposta> ListaDeGastosPorDia { get; set; } = [];
        public decimal Total { get; set; }
    }
}
