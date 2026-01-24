using ControleDeGastos.Data.ResultadoPaginado.RequisicaoPaginadaDTO;
using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.DTOs.Requisicao.GastosDiarios
{
    public class ObterGastosDiariosRequisicao : RequisicaoPaginada
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public DateTime InicioDoPeriodo { get; set; } = DateTime.MinValue;
        public DateTime FimDoPeriodo { get; set; } = DateTime.MinValue;
        public string Categoria { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
    }
}
