namespace ExpensesControl.Data.PaginatedResult
{
    /// <summary>
    /// Representa um resultado paginado de uma consulta, com itens e informações de paginação.
    /// </summary>
    public class PagedResult<T>
    {
        /// <summary>
        /// Itens retornados na página atual.
        /// </summary>
        public List<T> Items { get; }

        /// <summary>
        /// Número da página atual (1-based).
        /// </summary>
        public int CurrentPage { get; }

        /// <summary>
        /// Tamanho (quantidade) de itens por página.
        /// </summary>
        public int ItemsPerPage { get; }

        /// <summary>
        /// Total de itens na consulta completa.
        /// </summary>
        public int TotalItems { get; }

        /// <summary>
        /// Total de páginas calculado a partir de TotalItems e PageSize.
        /// </summary>
        public int TotalPages { get; }

        public PagedResult(List<T> items, int currentPage, int itemsPerPage, int totalItems)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
            if (currentPage < 1)
                throw new ArgumentOutOfRangeException(nameof(currentPage));
            if (itemsPerPage < 1)
                throw new ArgumentOutOfRangeException(nameof(itemsPerPage));
            if (totalItems < 0)
                throw new ArgumentOutOfRangeException(nameof(totalItems));

            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)itemsPerPage);
        }
    }
}
