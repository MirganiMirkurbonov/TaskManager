using Domain.Helpers;
using Domain.Models.Inner;

namespace Domain.Models.Response;

public class DefaultResponse<T>
{
    public bool Success { get; set; } = true;
    public ErrorResponse? Error { get; set; }
    public T? Result { get; set; }

    public DefaultResponse(T result)
    {
        if (HttpContextHelper.Current != null)
            HttpContextHelper.Current.Response.StatusCode = 200;
        Result = result;
    }

    public DefaultResponse(ErrorResponse? error)
    {
        Error = error;
        Success = false;
    }

    public static implicit operator DefaultResponse<T>(T result) => new(result);

    public static implicit operator DefaultResponse<T>(ErrorResponse error) => new(error);
}