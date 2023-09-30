using System.Net;
using System.Net.Http.Headers;
using Domain.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Helpers;

public class MyController<T> : ControllerBase where T : class
{
    public string ErrorMessage => GetErrorMessage();

    private string GetErrorMessage()
    {
        var errorMessages = ModelState.Values
            .SelectMany(v => v.Errors)
            .First();
        
        return errorMessages.ErrorMessage;
    }

    /// <summary>
    /// Gets the User Id from the Authorization header.
    /// </summary>
    /// <returns>The User Id if present, otherwise null.</returns>
    protected long? UserId => GetUserId();

    private static long? GetUserId()
    {
        var httpContext = HttpContextHelper.Current;

        try
        {
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
                return null;

            var authorizationHeader = AuthenticationHeaderValue.Parse(httpContext.Request.Headers["Authorization"]);

            // Check if the authorization scheme starts with "Bearer".
            if (authorizationHeader.Scheme.StartsWith("Bearer"))
            {
                // Attempt to extract the UserId from the User's claims.
                var userId = Convert.ToInt64(httpContext.User.Claims?.FirstOrDefault(s => s.Type == "UserId")?.Value ?? "0");

                return userId;
            }

            // If the authorization scheme is not "Bearer," return Unauthorized status.
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            httpContext.Response.WriteAsync("Unauthorized!");
            return null;
        }
        catch (Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.WriteAsync("An error occurred while processing the request.");

            return null;
        }
    }

}