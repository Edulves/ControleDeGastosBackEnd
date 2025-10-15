namespace ControleDeGastos.DTOs.Respostas.ConsolidadoRespostas
{
    public class ObterGastosDiariosConsolidadosPorCategoriaComTotaisResposta
    {
        public List<ObterGastosDiariosConsolidadosPorCategoriasResposta> ListaDeGastosPorCategoria { get; set; } = new List<ObterGastosDiariosConsolidadosPorCategoriasResposta>();
        public decimal TotalDeGastos { get; set; }
    }
}
