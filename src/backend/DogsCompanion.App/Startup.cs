using DogsCompanion.App.Authentication;
using DogsCompanion.App.Settings;
using DogsCompanion.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DogsCompanion.App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var securitySettingsSection = Configuration.GetSection("SecuritySettings");
            services.Configure<SecuritySettings>(securitySettingsSection);
            var securitySettings = securitySettingsSection.Get<SecuritySettings>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                // Указывает, будет ли валидироваться издатель при валидации токена
                ValidateIssuer = true,

                // Будет ли валидироваться потребитель токена
                ValidateAudience = false,

                // Будет ли валидироваться время существования
                ValidateLifetime = true,

                // Строка, представляющая издателя
                ValidIssuer = securitySettings.JwtIssuer,

                // Установка потребителя токена
                ValidAudience = securitySettings.JwtAudience,

                // Валидация ключа безопасности
                ValidateIssuerSigningKey = true,

                // Установка ключа безопасности
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitySettings.JwtSecret)),
                ClockSkew = TimeSpan.Zero // Удалить задержку после истечения времени токена
            };

            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;

                    options.TokenValidationParameters = tokenValidationParameters;

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Is-Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddControllers();

            services.AddSingleton(new JwtManager(securitySettings));
            services.AddHttpContextAccessor();
            services.AddScoped<ClaimsValidationService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Dog's Companion API",
                    Version = "v1",
                    Description = "ASP.NET Core Web API для приложения Dog's Companion"
                });

                c.CustomOperationIds(apiDescription =>
                {
                    return apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Please insert JWT with Bearer into field",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey // TODO SecuritySchemeType.Http
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[] { } }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFilename);
                c.IncludeXmlComments(xmlPath);
            });

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DogsCompanionContext>(options => options.UseSqlServer(
                connectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DogAssistant v1");
                    c.DisplayOperationId();
                });
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
