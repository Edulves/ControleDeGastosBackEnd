namespace ControleDeGastos.DTOs.Requisicoes.ConsolidadoRequisicoes
{
    public class GetByFullDateMothDayRequest
    {
        public DateTime BeginningOfPeriod { get; set; }
        public DateTime EndOfPeriod { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
