using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Caching.Memory;
using MimeKit;
using MimeKit.Text;

namespace Lotto.Service.Helpers;

public static class EmailHelper
{
    public static async ValueTask SendMessageAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(EnvironmentHelper.EmailAddress));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart(TextFormat.Html) { Text = body };

        var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            EnvironmentHelper.SmtpHost,
            Convert.ToInt32(EnvironmentHelper.SmtpPort),
            SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync(
            EnvironmentHelper.EmailAddress,
            EnvironmentHelper.EmailPassword);

        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }

    public async static ValueTask SendCodeAsync(IMemoryCache memoryCache, string email, string key)
    {
        var random = new Random();
        var code = random.Next(10000, 99999);

        await SendMessageAsync(email, "", code.ToString());

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5))  
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)) 
            .SetPriority(CacheItemPriority.Normal)
            .SetSize(1024);

        memoryCache.Set(key, code, cacheOptions);
    }

}
