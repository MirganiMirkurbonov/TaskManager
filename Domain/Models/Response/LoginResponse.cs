namespace Domain.Models.Response;

public record LoginResponse(string Token, DateTime ExpireDate);