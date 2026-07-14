namespace ExpensesControl.Repositories.RepositoriesInterface
{
    public interface IGenericOperationsRepository
    {
        #region MetodosComuns
        Task<T> CreateAsync<T>(T entidade) where T : class;
        Task<T> UpdateAsync<T>(T entidade) where T : class;
        Task<List<T>> CreateAsync<T>(List<T> entidade) where T : class;
        Task<List<T>> UpdateAsync<T>(List<T> entidade) where T : class;
        #endregion
    }
}
