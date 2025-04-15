using System.Security.Cryptography;
using System.Text;

public static class EncryptionService
{
    private static readonly string EncryptionKey = "your-secure-32-char-key-here!!!"; // 32 baytli maxfiy kalit

    public static string Encrypt(int[] numbers)
    {
        string input = string.Join(",", numbers);
        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32, '\0'));
            aes.IV = RandomNumberGenerator.GetBytes(16);

            using (var encryptor = aes.CreateEncryptor())
            using (var ms = new MemoryStream())
            {
                ms.Write(aes.IV, 0, aes.IV.Length);
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(input);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public static int[] Decrypt(string encrypted)
    {
        byte[] cipherBytes = Convert.FromBase64String(encrypted);
        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32, '\0'));
            byte[] iv = cipherBytes.Take(16).ToArray();
            aes.IV = iv;

            using (var decryptor = aes.CreateDecryptor())
            using (var ms = new MemoryStream(cipherBytes, 16, cipherBytes.Length - 16))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                var decrypted = sr.ReadToEnd();
                return decrypted.Split(',').Select(int.Parse).ToArray();
            }
        }
    }
}