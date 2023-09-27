using System.Security.Cryptography;
using System.Text;
using Domain.Models.Inner;

namespace Domain.Helpers;

public class CryptoHelper
{
    public const int SALT_SIZE = 32;
    public const int HASH_SIZE = 64;
    public const int ITERATIONS = 1000;


    public static HashSalt CreateHashSalted(string password)
    {
        try
        {
            var saltBytes = new byte[SALT_SIZE];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            var salt = Convert.ToBase64String(saltBytes);

            using var rfc2898DeriveBytes =
                new Rfc2898DeriveBytes(password, saltBytes, ITERATIONS, HashAlgorithmName.SHA256);
            var hashBytes = rfc2898DeriveBytes.GetBytes(HASH_SIZE);

            var hashPassword = Convert.ToBase64String(hashBytes);

            return new HashSalt
            {
                Hash = hashPassword,
                Salt = salt
            };
        }
        catch
        {
            return null;
        }
    }

    public static string GetHashSalted(string password, string salt)
    {
        try
        {
            var bsalt = Encoding.Default.GetBytes(salt);
            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, bsalt, ITERATIONS);

            return ByteArrayToString(rfc2898DeriveBytes.GetBytes(HASH_SIZE));
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