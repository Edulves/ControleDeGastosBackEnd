using ControleDeGastos.Data.ResultadoPaginado.RequisicaoPaginadaDTO;

namespace ControleDeGastos.DTOs.Requisicoes.GastosFixosRequisicoes
{
    public class GetFixedExpensesRequest : RequisicaoPaginada
    {
        public DateTime InicioDoPeriodo { get; set; }
        public DateTime FimDoPeriodo { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string DescricaoDoGasto { get; set; } = string.Empty;
    }
}
