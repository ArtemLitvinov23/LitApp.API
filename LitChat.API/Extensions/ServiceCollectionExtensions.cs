using LitChat.API.Mapper;
using LitChat.BLL.Jwt;
using LitChat.BLL.Jwt.Interfaces;
using LitChat.BLL.Mapper;
using LitChat.BLL.Services;
using LitChat.BLL.Services.Interfaces;
using LitChat.DAL.Repositories;
using LitChat.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LitChat.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AllServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BLMapperProfile), typeof(PLMapperProfile));

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IFavoritesRepository, FavoritesRepository>();
            services.AddScoped<IFavoritesService, FavoritesService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IConnectionRepository, ConnectionRepository>();
            services.AddScoped<IConnectionService, ConnectionService>();
            return services;
        }
    }
}
