using System.Runtime.InteropServices.JavaScript;

namespace Domain.Models.Inner;

public record JwtGeneratedResponse(String Token, DateTime ExpireDate);