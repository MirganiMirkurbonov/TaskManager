namespace Domain.Models.Response.Auth;

public record LoginResponse(
    string Token,
    DateTime ExpireDate);