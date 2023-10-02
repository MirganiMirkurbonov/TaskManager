namespace Domain.Enumerators;

public enum EResponseCode
{
    InvalidInputParams,
    InternalServerError,
    UsernameAlreadyExists,
    EmailAlreadyRegistered,
    UsernameOrEmailAlreadyExists,
    UserNotFound,
    InvalidPassword
}