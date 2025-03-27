namespace Lotto.Service.Helpers;

public static class EnvironmentHelper
{
    public static string JwtKey { get; set; }
    public static string TokenLifeTimeInHour { get; set; }
    public static string SmtpHost { get; set; }
    public static string SmtpPort { get; set; }
    public static string EmailAddress { get; set; }
    public static string EmailPassword { get; set; }
    public static string WebRootPath { get; set; }
    public static string SuperAdminLogin { get; set; }
    public static string SuperAdminPassword { get; set; }
}
