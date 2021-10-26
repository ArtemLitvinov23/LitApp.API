using System.Threading.Tasks;
using LitBlog.API.Helpers;
using LitBlog.API.Middleware;
using LitBlog.BLL.Jwt;
using LitBlog.BLL.Mapper;
using LitBlog.BLL.Middleware;
using LitBlog.BLL.PasswordHasher;
using LitBlog.BLL.Services;
using LitBlog.BLL.Services.Interfaces;
using LitBlog.BLL.Settings;
using LitBlog.DAL;
using LitBlog.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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


            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddAutoMapper(typeof(MapperProfile), typeof(AutoMapperProfile));

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddTransient<IJwtOptions, JwtService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IEmailService, EmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BlogContext context)
        {
            context.Database.Migrate();

            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET LitBlog API"));
            app.UseRouting();

            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/{**path}", context =>
                {
                    if (!env.IsProduction())
                    {
                        context.Response.Redirect("/swagger");
                        return Task.CompletedTask;
                    }

                    context.Response.WriteAsync("Not Found");
                    return Task.CompletedTask;
                });

            });
        }
    }
}
