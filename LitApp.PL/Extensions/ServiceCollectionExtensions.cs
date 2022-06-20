using LitApp.BLL.Mapper;
using LitApp.BLL.ModelsDto;
using LitApp.BLL.Services;
using LitApp.BLL.Services.Interfaces;
using LitApp.DAL.Repositories;
using LitApp.DAL.Repositories.Interfaces;
using LitApp.PL.Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LitApp.PL.Extensions
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

            services.AddMemoryCache();

            return services;
        }
    }
}
