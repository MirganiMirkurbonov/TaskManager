using System.Security.Cryptography;
using System.Text;
using Domain.Models.Inner;

namespace Domain.Helpers;

public class CryptoHelper
{
    private const int SaltSize = 32;
    private const int HashSize = 64;
    private const int Iterations = 1000;

    public static HashSalt CreateHashSalted(string password)
    {
        var saltBytes = new byte[SaltSize];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(saltBytes);

        var salt = Convert.ToBase64String(saltBytes);

        using var rfc2898DeriveBytes =
            new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
        var hashBytes = rfc2898DeriveBytes.GetBytes(HashSize);

        var hashPassword = Convert.ToBase64String(hashBytes);

        return new HashSalt
        {
            Hash = hashPassword,
            Salt = salt
        };
    }

    public static string GetHashSalted(string password, string salt)
    {
        try
        {
            var bsalt = Encoding.Default.GetBytes(salt);
            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, bsalt, Iterations);

            return ByteArrayToString(rfc2898DeriveBytes.GetBytes(HashSize));
        }
        catch
        {
            return string.Empty;
        }
    }

    private static string ByteArrayToString(byte[] ba)
    {
        try
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (var b in ba)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString();
        }
        catch
        {
            return string.Empty;
        }
    }
}