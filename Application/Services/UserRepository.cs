using System.Net;
using Application.Common;
using Application.Extensions;
using Application.Interfaces;
using Domain.Enumerators;
using Domain.Extensions;
using Domain.Helpers;
using Domain.Models.Inner;
using Domain.Models.Request;
using Domain.Models.Request.Auth;
using Domain.Models.Response;
using Domain.Models.Response.Auth;
using Domain.Schemas.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Common;

namespace Application.Services;

internal class UserRepository : IUser
{
    private readonly ILogger<UserRepository> _logger;
    private readonly IGenericRepository<AuthUser> _userRepository;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public UserRepository(ILogger<UserRepository> logger,
        IGenericRepository<AuthUser> userRepository,
        IJwtTokenGenerator tokenGenerator)
    {
        _logger = logger;
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<DefaultResponse<LoginResponse>> SignupAsync(SignUpRequest request,
        string traceId,
        CancellationToken cancellationToken)
    {
        try
        {
            request = request.TrimAndLower();
            
            // check username and email to unique
            var usernameOrEmailAlreadyExists =await 
                _userRepository.Exists(x => x.Username == request.Username || x.Email == request.Email, cancellationToken);
            
            if (usernameOrEmailAlreadyExists)
                return new ErrorResponse(HttpStatusCode.BadRequest, EResponseCode.UsernameOrEmailAlreadyExists);
            
            var passwordHashSalted = CryptoHelper.CreateHashSalted(request.Password);

            // TODO: write special configuration for mapping
            var user = request.MapTo<AuthUser>();
            user.PasswordHash = passwordHashSalted.Hash;
            user.PasswordSalt = passwordHashSalted.Salt;
            
            var newUser = await _userRepository.Create(user, cancellationToken);

            var newToken = _tokenGenerator.GenerateTokenAsync(
                newUser.FirstName,
                newUser.LastName,
                newUser.Id.ToString());

            var result = newToken.MapTo<LoginResponse>();
            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(traceId, exception, $"Request: {request.ToJsonString()}", cancellationToken);
            return new ErrorResponse(HttpStatusCode.InternalServerError, EResponseCode.InternalServerError);
        }
    }
    
    public async Task<DefaultResponse<LoginResponse>> SignInAsync(SignInRequest request,
        string traceId,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository
                .QueryWithTracking()
                .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);
            if (user == null)
                return new ErrorResponse(HttpStatusCode.NotFound, EResponseCode.UserNotFound);

            var passwordCorrect = CryptoHelper.VerifyPassword(
                password: request.Password,
                storedHash: user.PasswordHash,
                storedSalt: user.PasswordSalt);
            
            if (!passwordCorrect)
                return new ErrorResponse(HttpStatusCode.BadRequest, EResponseCode.InvalidPassword);

            var userToken = _tokenGenerator.GenerateTokenAsync(
                user.FirstName,
                user.LastName,
                user.Id.ToString());

            return userToken.MapTo<LoginResponse>();
        }
        catch (Exception exception)
        {
            _logger.LogError(traceId, exception, $"Request: {request.ToJsonString()}", cancellationToken);
            return new ErrorResponse(HttpStatusCode.InternalServerError, EResponseCode.InternalServerError);
        }
    }
}