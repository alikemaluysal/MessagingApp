using System.Security.Cryptography;
using System.Text;

namespace Core.Application.Security;

public static class HashingHelper
{
    public static (byte[] Hash, byte[] Salt) CreatePasswordHash(string password)
    {
        using HMACSHA512 hmac = new HMACSHA512();
        byte[] salt = hmac.Key;
        byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return (hash, salt);
    }


    public static bool VerifyPassworHash(string password, byte[] hash, byte[] salt)
    {
        using HMACSHA512 hmac = new HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(hash);
    }
}
