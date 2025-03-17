using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;


namespace Lotto.Service.Helpers;
public class ZipService
{
    public string CreateWinningNumbersArchive(string gameId, string winningNumbers)
    {
        string folderPath = Path.Combine("wwwroot", "archives", gameId);
        string zipPath = folderPath + ".zip.enc";
        string textFilePath = Path.Combine(folderPath, "winning_numbers.txt");
        string password = GenerateRandomPassword();

        Directory.CreateDirectory(folderPath);
        File.WriteAllText(textFilePath, winningNumbers);

        // ZIP yaratish
        ZipFile.CreateFromDirectory(folderPath, folderPath + ".zip");

        // Shifrlash
        EncryptFile(folderPath + ".zip", zipPath, password);

        // Asl ZIP va temporary fayllarni o‘chirish
        Directory.Delete(folderPath, true);
        File.Delete(folderPath + ".zip");

        return zipPath; // ZIP faylni userlarga berish uchun link sifatida qaytaramiz
    }

    private void EncryptFile(string inputPath, string outputPath, string password)
    {
        using (var aes = Aes.Create())
        {
            using (var sha256 = SHA256.Create())
            {
                aes.Key = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                aes.IV = aes.Key[..16];
            }

            using (var fileStream = new FileStream(outputPath, FileMode.Create))
            using (var cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (var inputFileStream = new FileStream(inputPath, FileMode.Open))
            {
                inputFileStream.CopyTo(cryptoStream);
            }
        }
    }

    private string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 16) // 16 ta belgili parol yaratamiz
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

}
