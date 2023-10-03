using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using Domain.Helpers;
using Domain.Models.Request;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace API.Helpers;

public class MyController<T> : ControllerBase where T : class
{
    /// <summary>
    /// Gets the User Id from the Authorization header.
    /// </summary>
    /// <returns>The User Id if present, otherwise null.</returns>
    protected long? UserId => GetUserId();
    /// <summary>
    /// TraceId
    /// </summary>
    protected string TraceId => HttpContext.TraceIdentifier;
    /// <summary>
    /// 
    /// </summary>
    protected CancellationToken CancellationToken => HttpContext.RequestAborted;

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
                var userId = Convert.ToInt64(httpContext.User.Claims?.FirstOrDefault(s => s.Type == "UserId")?.Value ?? "0");
                
                if (userId != 0)
                    return userId;
                
                HttpContextHelper.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                 httpContext.Response.WriteAsync("Unauthorized");
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