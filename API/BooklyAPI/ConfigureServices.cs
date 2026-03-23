using System.Text;
using Domain.Enums;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BooklyAPI;

public static class ConfigureServices
{
    public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        object value = services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        services.AddAuthentication().AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[ConfigConsts.IdentitySecret])),
                ValidIssuer = configuration[ConfigConsts.IdentityHost],
                ValidAudience = configuration[ConfigConsts.IdentityValidAudience]
            };
        });

        services.AddCors(p => p.AddPolicy("corspolicy", builder =>
        {
            builder.WithOrigins("http://localhost:5173",  "https://bookly.mbogdan.pl")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials()
                   .WithExposedHeaders("Content-Disposition");
        }));

        services.AddControllers();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("api", new OpenApiInfo { Title = "Bookly API" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }
}
