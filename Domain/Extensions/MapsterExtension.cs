using Mapster;

namespace Domain.Extensions;

public static class MapsterExtension
{
    public static T MapTo<T>(this object source) where T : class =>
        source.Adapt<T>();
}