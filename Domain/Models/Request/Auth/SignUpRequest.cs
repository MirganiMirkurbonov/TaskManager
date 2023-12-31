﻿namespace Domain.Models.Request.Auth;

public class SignUpRequest
{
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}