using Domain.Enumerators;

namespace Domain.Helpers;

public static class LocalizationHelper
{
    public static string Localize(this EResponseCode responseCode)
    {
        // TODO: localize error enums
        return responseCode.ToString();
    }
}