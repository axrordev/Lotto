using Lotto.Data.UnitOfWorks;
using Lotto.Service.Helpers;
using Lotto.Service.Services.Advertisements;
using Lotto.Service.Services.UserRolePermissions;
using Lotto.Service.Services.UserRoles;
using Lotto.WebApi.ApiServices;
using Lotto.WebApi.ApiServices.Advertisements;
using Lotto.WebApi.ApiServices.Numbers;
using Lotto.WebApi.Helpers;
using Lotto.WebApi.Middlewares;
using Lotto.WebApi.Validators.Accounts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.IO;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Lotto.Service.Services.Users;
using Lotto.Service.Services.Accounts;
using Lotto.Service.Services.Permissions;
using Lotto.WebApi.ApiServices.Accounts;
using Lotto.WebApi.ApiServices.Users;
using Lotto.WebApi.ApiServices.Permissions;
using Lotto.WebApi.ApiServices.UserRoles;
using Lotto.WebApi.ApiServices.UserRolePermissions;
using Microsoft.Extensions.Logging;
using Lotto.Service.Services.Footballs;
using Lotto.WebApi.ApiServices.Footballs;
using Lotto.Service.Services.Comments;
using Lotto.Service.Services.CommentSettings;
using Lotto.WebApi.ApiServices.Comments;
using Lotto.WebApi.ApiServices.CommentServices;


namespace Lotto.WebApi.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAdvertisementService, AdvertisementService>();
            services.AddScoped<INumberService, NumberService>();
            services.AddScoped<IFootballService, FootballService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentSettingService, CommentSettingService>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserRolePermissionService, UserRolePermissionService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<INumberApiService, NumberApiService>();
            services.AddScoped<IFootballApiService, FootballApiService>();
            services.AddScoped<IAdvertisementApiService, AdvertisementApiService>();
             services.AddScoped<ICommentApiService, CommentApiService>();
            services.AddScoped<ICommentSettingApiService, CommentSettingApiService>();

            services.AddScoped<IAccountApiService, AccountApiService>();
            services.AddScoped<IUserApiService, UserApiService>();
            services.AddScoped<IPermissionApiService, PermissionApiService>();
            services.AddScoped<IUserRoleApiService, UserRoleApiService>();
            services.AddScoped<IUserRolePermissionApiService, UserRolePermissionApiService>();
        }

        public static void AddExceptions(this IServiceCollection services)
        {
            services.AddExceptionHandler<NotFoundExceptionMiddleware>();
            services.AddExceptionHandler<ForbiddenExceptionMiddleware>();
            services.AddExceptionHandler<AlreadyExistExceptionMiddleware>();
            services.AddExceptionHandler<InternalServerExceptionMiddleware>();
            services.AddExceptionHandler<ArgumentIsNotValidExceptionMiddleware>();
        }

        public static void AddInjectHelper(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            ServiceHelper.UserRoleService = scope.ServiceProvider.GetRequiredService<IUserRoleService>();
            ServiceHelper.UserRolePermissionService = scope.ServiceProvider.GetRequiredService<IUserRolePermissionService>();
        }

        public static void AddPathInitializer(this WebApplication app)
        {
            HttpContextHelper.ContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
            EnvironmentHelper.JwtKey = app.Configuration.GetSection("Jwt:Key").Value;
            EnvironmentHelper.TokenLifeTimeInHour = app.Configuration.GetSection("Jwt:LifeTime").Value;
            EnvironmentHelper.SmtpHost = app.Configuration.GetSection("Email:SmtpHost").Value;
            EnvironmentHelper.SmtpPort = app.Configuration.GetSection("Email:SmtpPort").Value;
            EnvironmentHelper.EmailAddress = app.Configuration.GetSection("Email:EmailAddress").Value;
            EnvironmentHelper.EmailPassword = app.Configuration.GetSection("Email:EmailPassword").Value;
            EnvironmentHelper.SuperAdminLogin = app.Configuration.GetSection("SuperAdmin:Login").Value;
            EnvironmentHelper.SuperAdminPassword = app.Configuration.GetSection("SuperAdmin:Password").Value;
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<AccountLoginValidator>();
            services.AddTransient<AccountCreateValidator>();
            services.AddTransient<AccountVerifyValidator>();
            services.AddTransient<AccountSendCodeValidator>();
            services.AddTransient<AccountRegisterModelValidator>();
            services.AddTransient<AccountResetPasswordValidator>();
        }

        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            // JWT autentifikatsiyasi uchun Swagger konfiguratsiyasi
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });        
        });
    }

    }
}
