namespace ControleDeGastos.Data.PadraoDeResposta.Base
{
    public class RespostaPadrao
    {
        /// <summary>
        /// Representa o resultado de uma operação que pode ser bem-sucedida ou conter um erro.
        /// Utilizada para evitar exceções e propagar erros de maneira controlada.
        /// </summary>
        public class Result<T>
        {
            public bool IsSuccess { get; }
            public T Value { get; }
            public int? StatusCode { get; }
            public string Title { get; }
            public List<string> ErrorMessages { get; }

            protected Result(
                bool isSuccess,
                T value,
                int? statusCode,
                string title,
                List<string> ErrorMessage)
            {
                IsSuccess = isSuccess;
                Value = value;
                StatusCode = statusCode;
                Title = title;
                this.ErrorMessages = ErrorMessage;
            }

            #region Result - Sucess
            public static Result<T> Success(T value) => new Result<T>(
                    isSuccess: true,
                    value: value,
                    statusCode: StatusCodes.Status200OK,
                    title: null,
                    ErrorMessage: new List<string>());
            #endregion

            #region Result - Failure
            /// <summary>
            /// Cria um resultado de falha, onde a primeira mensagem de erro é obrigatória.
            /// </summary>
            /// <param name="errorMessage">Mensagem principal de erro.</param>
            /// <param name="statusCode">Código HTTP a ser retornado (default 400).</param>
            /// <param name="title">Título do problema (default "Erro").</param>
            /// <param name="otherErrorMessages">Mensagens de erro adicionais.</param>
            public static Result<T> Failure(
                string errorMessage,
                int statusCode = StatusCodes.Status400BadRequest,
                string title = "Erro",
                params string[] otherErrorMessages)
            {
                var allErrors = new List<string> { errorMessage };
                if (otherErrorMessages != null && otherErrorMessages.Any())
                    allErrors.AddRange(otherErrorMessages);

                return new Result<T>(
                    isSuccess: false,
                    value: default,
                    statusCode: statusCode,
                    title: title,
                    ErrorMessage: allErrors);
            }
            #endregion
        }
    }
}
