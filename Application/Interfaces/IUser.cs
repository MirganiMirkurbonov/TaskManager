using Domain.Models.Request;
using Domain.Models.Request.Auth;
using Domain.Models.Response;
using Domain.Models.Response.Auth;

namespace Application.Interfaces;

public interface IUser
{
    Task<DefaultResponse<LoginResponse>> SignupAsync(SignUpRequest request, string traceId, CancellationToken cancellationToken);
    Task<DefaultResponse<LoginResponse>> SignInAsync(SignInRequest request, string traceId, CancellationToken cancellationToken);
}