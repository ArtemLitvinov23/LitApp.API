using LitBlog.API.Helpers;
using LitBlog.BLL.Jwt;
using LitBlog.BLL.Mapper;
using LitBlog.BLL.PasswordHasher;
using LitBlog.BLL.Services;
using LitBlog.BLL.Services.Interfaces;
using LitBlog.BLL.Settings;
using LitBlog.DAL;
using LitBlog.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.Tasks;

namespace LitBlog.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BlogContext>(opt=> opt.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase")));
            services.AddCors();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LitBlog.WebApi", Version = "v1" });
            });

            services.AddSignalR();
            services.Configure<EmailSettings>(Configuration.GetSection("AppSettings"));
            services.AddAutoMapper(typeof(MapperProfile), typeof(AutoMapperProfile));

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddTransient<IJwtOptions, JwtService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFavoritesRepository, FavoritesRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatService, ChatService>();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JwtConfig").GetSection("Secret").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET LitBlog API"));

            app.UseRouting();

            app.UseCors(cors => cors
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .SetIsOriginAllowed(origin => true)
                   .AllowCredentials()
                    );
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/SignalRHub");
            });

        }
    }
}
