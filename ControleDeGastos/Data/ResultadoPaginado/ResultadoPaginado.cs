namespace ControleDeGastos.Data.ResultadoPaginado
{
    /// <summary>
    /// Representa um resultado paginado de uma consulta, com itens e informações de paginação.
    /// </summary>
    public class ResultadoPaginado<T>
    {
        /// <summary>
        /// Itens retornados na página atual.
        /// </summary>
        public List<T> Itens { get; }

        /// <summary>
        /// Número da página atual (1-based).
        /// </summary>
        public int PaginaAtual { get; }

        /// <summary>
        /// Tamanho (quantidade) de itens por página.
        /// </summary>
        public int ItensPorPagina { get; }

        /// <summary>
        /// Total de itens na consulta completa.
        /// </summary>
        public int TotalItens { get; }

        /// <summary>
        /// Total de páginas calculado a partir de TotalItems e PageSize.
        /// </summary>
        public int TotalDePaginas { get; }

        public ResultadoPaginado(List<T> itens, int paginaAtual, int itensPorPagina, int totalItens)
        {
            Itens = itens ?? throw new ArgumentNullException(nameof(itens));
            if (paginaAtual < 1)
                throw new ArgumentOutOfRangeException(nameof(paginaAtual));
            if (itensPorPagina < 1)
                throw new ArgumentOutOfRangeException(nameof(itensPorPagina));
            if (totalItens < 0)
                throw new ArgumentOutOfRangeException(nameof(totalItens));

            PaginaAtual = paginaAtual;
            ItensPorPagina = itensPorPagina;
            TotalItens = totalItens;
            TotalDePaginas = (int)Math.Ceiling(totalItens / (double)itensPorPagina);
        }
    }
}
