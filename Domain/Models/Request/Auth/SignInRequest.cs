namespace Domain.Models.Request.Auth;

public class SignInRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}