namespace ControleDeGastos.DTOs.Requisicoes.ConsolidadoRequisicoes
{
    public class ObterGastosDiariosConsolidadosPorCategoriaRequisicao
    {
        public DateTime InicioDoPeriodo { get; set; }
        public DateTime FimDoPeriodo { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
    }
}
