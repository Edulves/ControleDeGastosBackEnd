using ControleDeGastos.Data.ResultadoPaginado.RequisicaoPaginadaDTO;

namespace ControleDeGastos.DTOs.Requisicoes.GastosFixosRequisicoes
{
    public class ObterGastosFixosRequisicao : RequisicaoPaginada
    {
        public DateTime InicioDoPeriodo { get; set; }
        public DateTime FimDoPeriodo { get; set; }
        public string DescricaoDoGasto { get; set; } = string.Empty;
    }
}
