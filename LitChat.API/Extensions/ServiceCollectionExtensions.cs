using LitChat.API.Mapper;
using LitChat.BLL.Mapper;
using LitChat.BLL.ModelsDto;
using LitChat.BLL.Services;
using LitChat.BLL.Services.Interfaces;
using LitChat.DAL.Models;
using LitChat.DAL.Repositories;
using LitChat.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LitChat.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(BLMapperProfile), typeof(PLMapperProfile));

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddTransient<IJwtService, TokenService>();
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
            services.AddScoped<IFriendRepository, FriendRepository>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<ICacheService<AccountResponseDto>, CacheService>();

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = configuration["Redis:InstanceName"];
            });

            return services;
        }
    }
}
