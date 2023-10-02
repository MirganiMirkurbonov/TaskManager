using System.Net;
using API.Helpers;
using Application.Interfaces;
using Domain.Enumerators;
using Domain.Models.Inner;
using Domain.Models.Request;
using Domain.Models.Response;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
//[Authorize]
public class UserController : MyController<UserController>
{
    /// <summary>
    /// Validator for validating sign-up requests before processing.
    /// </summary>
    private readonly IValidator<SignUpRequest> _signUpValidator;

    private readonly IValidator<SignInRequest> _signInValidator;
    private readonly IUser _user;

    public UserController(
        IValidator<SignUpRequest> signUpValidator,
        IValidator<SignInRequest> signInValidator,
        IUser user)
    {
        _signUpValidator = signUpValidator;
        _signInValidator = signInValidator;
        _user = user;
    }

    [HttpPost("sign-up")]
    public async Task<DefaultResponse<LoginResponse>> SignUp(SignUpRequest request)
    {
        var validationResult = await _signUpValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return new ErrorResponse(HttpStatusCode.BadRequest, EResponseCode.InvalidInputParams);
        
        return await _user.SignupAsync(request, TraceId, CancellationToken);
    }

    [HttpPost("log-in")]
    public async Task<DefaultResponse<LoginResponse>> LogIn(SignInRequest request)
    {
        var validationResult = await _signInValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return new ErrorResponse(HttpStatusCode.BadRequest, EResponseCode.InvalidInputParams);

        return await _user.SignInAsync(request, TraceId, CancellationToken);
    }
}