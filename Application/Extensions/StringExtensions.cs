using System.Reflection;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Application.Extensions;

public static class StringExtensions
{
    public static string ToJsonString<T>(this T param)
    {
        return JsonConvert.SerializeObject(param);
    }
    
    public static T TrimAndLower<T>(this T obj) where T : class
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.CanWrite);

        foreach (var property in properties)
        {
            var value = property.GetValue(obj);

            if (value is not string strValue) continue;

            // Trim all string properties
            var trimmedValue = strValue.Trim();
            property.SetValue(obj, trimmedValue);

            // Convert "Username" and "Email" properties to lowercase
            if (property.Name.Equals("Username", StringComparison.OrdinalIgnoreCase) ||
                property.Name.Equals("Email", StringComparison.OrdinalIgnoreCase))
            {
                property.SetValue(obj, trimmedValue.ToLowerInvariant());
            }
        }

        return obj;
    }
}