using System.Security.Cryptography;
using System.Text;

public static class HashService
{
    public static string GenerateHash(int[] numbers)
    {
        if (numbers == null || numbers.Length != 5)
            throw new ArgumentException("Numbers must contain exactly 5 numbers.");

        string input = string.Join(",", numbers);
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    public static bool VerifyHash(int[] numbers, string hash)
    {
        var computedHash = GenerateHash(numbers);
        return computedHash == hash;
    }
}