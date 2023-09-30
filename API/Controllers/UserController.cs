using System.Net;
using API.Helpers;
using Domain.Enumerators;
using Domain.Models.Inner;
using Domain.Models.Request;
using Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
//[Authorize]
public class UserController : MyController<UserController>
{
    [HttpPost("sign-up")]
    public async Task<DefaultResponse<LoginResponse>> SignUp(SignUpRequest request)
    {
        if (!ModelState.IsValid)
        {
            return new ErrorResponse(HttpStatusCode.BadRequest, EResponseCode.InvalidInputParams);
        }

        return new LoginResponse("test", DateTime.Now);
    }
}