using LitBlog.API.Helpers;
using LitBlog.BLL.Jwt;
using LitBlog.BLL.Mapper;
using LitBlog.BLL.PasswordHasher;
using LitBlog.BLL.Services;
using LitBlog.DAL.Repositories;
using LitChat.BLL.Services;
using LitChat.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LitChat.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AllServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile), typeof(AutoMapperProfile));

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddTransient<IJwtOptions, JwtService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IFavoritesRepository, FavoritesRepository>();
            services.AddScoped<IFavoritesService, FavoritesService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IConnectionRepository, ConnectionRepository>();
            services.AddScoped<IConnectionService, ConnectionService>();
            return services;
        }
    }
}
