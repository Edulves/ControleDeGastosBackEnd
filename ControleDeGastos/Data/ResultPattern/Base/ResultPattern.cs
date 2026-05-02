namespace ControleDeGastos.Data.PadraoDeResposta.Base;

public class ResultPattern<T>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public int StatusCode { get; }
    public string Title { get; }
    public string Detail { get; }


    protected ResultPattern(bool isSuccess, T value, int statusCode, string title, string detail)
    {
        IsSuccess = isSuccess;
        Value = value;
        StatusCode = statusCode;
        Title = title;
        Detail = detail;
    }


    public static ResultPattern<T> Success(T value, int StatusCode = StatusCodes.Status200OK) => new ResultPattern<T>
    (
        isSuccess: true,
        value: value,
        statusCode: StatusCode,
        title: "Sucess",
        detail: "Sucess"
    );


    public static ResultPattern<T> Failure(T value, string errorMessage, string title = "Erro", int statusCode = StatusCodes.Status400BadRequest )
    {
        return new ResultPattern<T>
            (
                isSuccess: false,
                value: value,
                statusCode: statusCode,
                title: title,
                detail: errorMessage
            );
    }
}
