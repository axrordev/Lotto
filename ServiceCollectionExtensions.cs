public static void AddServices(this IServiceCollection services, IConfiguration configuration)
{
    services.AddScoped<IAdvertisementService, AdvertisementService>();
    services.AddScoped<INumberService, NumberService>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IPermissionService, PermissionService>();
    services.AddScoped<IUserRoleService, UserRoleService>();
    services.AddScoped<IUserRolePermissionService, UserRolePermissionService>();

    services.AddScoped<ZipService>(provider => new ZipService(configuration["WwwRootPath"]));
}
