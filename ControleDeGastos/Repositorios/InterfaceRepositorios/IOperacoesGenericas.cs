namespace ControleDeGastos.Repositorios.InterfaceRepositorios
{
    public interface IOperacoesGenericas
    {
        #region MetodosComuns
        Task<T> CriarAsync<T>(T entidade) where T : class;
        Task<T> AtualizarAsync<T>(T entidade) where T : class;
        Task<List<T>> CriarAsync<T>(List<T> entidade) where T : class;
        Task<List<T>> AtualizarAsync<T>(List<T> entidade) where T : class;
        #endregion
    }
}
