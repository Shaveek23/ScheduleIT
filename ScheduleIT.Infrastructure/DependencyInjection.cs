using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduleIT.Application.Core.Abstractions.Authentication;
using ScheduleIT.Application.Core.Abstractions.Common;
using ScheduleIT.Infrastructure.Authentication.Settings;
using ScheduleIT.Infrastructure.Authentication;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ScheduleIT.Domain.Services;
using ScheduleIT.Application.Core.Abstractions.Cryptography;
using ScheduleIT.Infrastructure.Cryptography;
using ScheduleIT.Infrastructure.Common;
using ScheduleIT.Infrastructure.BackgroundJobs.Configuration;
using ScheduleIT.Application.Core.Abstractions.Messaging;
using ScheduleIT.Infrastructure.Messaging;
using ScheduleIT.Infrastructure.Messaging.EventReminder.Infrastructure.Messaging.Settings;

namespace ScheduleIT.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]))
                });

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SettingsKey));

            services.ConfigureQuartzJobs();

            services.Configure<MessageBrokerSettings>(configuration.GetSection(MessageBrokerSettings.SettingsKey));

            services.AddScoped<IEmployeeIdentifierProvider, EmployeeIdentifierProvider>();

            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddTransient<IDateTime, MachineDateTime>();

            services.AddTransient<IPasswordHasher, PasswordHasher>();

            services.AddTransient<IPasswordHashChecker, PasswordHasher>();

            services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();

            return services;
        }
    }
}
