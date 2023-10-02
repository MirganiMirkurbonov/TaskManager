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
        try
        {
            var saltBytes = new byte[SaltSize];
            using var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);

            var salt = ByteArrayToString(saltBytes);
            var byteValue = Encoding.UTF8.GetBytes(salt);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, byteValue, Iterations);
            var hashPassword = ByteArrayToString(rfc2898DeriveBytes.GetBytes(HashSize));

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

    public static bool VerifyPassword( string password, string storedHash,string storedSalt)
    {
        try
        {
            var bsalt = Encoding.Default.GetBytes(storedSalt);
            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, bsalt, Iterations);
            var hash = ByteArrayToString(rfc2898DeriveBytes.GetBytes(HashSize));
            
            return hash == storedHash;
        }
        catch
        {
            return false;
        }
    }

    public static string ByteArrayToString(byte[] ba)
    {
        try
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString();
        }
        catch
        {
            return string.Empty;
        }
    }

    public static byte[] StringToByteArray(string hex)
    {
        try
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];

            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            return bytes;
        }
        catch
        {
            return null;
        }
    }
}
