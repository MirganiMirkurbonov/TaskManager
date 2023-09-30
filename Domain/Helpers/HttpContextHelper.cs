using System.Net;
using Microsoft.AspNetCore.Http;

namespace Domain.Helpers;

public class HttpContextHelper
{
    public static IHttpContextAccessor? Accessor;

    public static HttpContext Current => Accessor?.HttpContext;

    public static void SetStatusCode(HttpStatusCode code)
    {
        if (Current?.Response != null)
            Current.Response.StatusCode = (int)code;
    }
}