using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ScheduleIt.Application.Core.Abstractions.Persistance;
using ScheduleIT.Domain.Aggregates.Employee;
using ScheduleIT.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ScheduleIT.Persistence.Repositories.EmployeeRepo;
using ScheduleIT.Persistence.Interceptors;
using Microsoft.Extensions.Options;
using ScheduleIT.Persistence.Repositories.TeamRepo;
using ScheduleIT.Domain.Aggregates.Team;


namespace ScheduleIT.Persistence
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey);

            services.AddSingleton(new ConnectionString(connectionString));

            //services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

            //services.AddDbContext<ScheduleITDbContext>(
            //    (sp, optionsBuilder) =>
            //    {
            //        var interceptor = sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            //        optionsBuilder.UseSqlServer(connectionString).AddInterceptors(interceptor);
            //    });

            services.AddDbContext<ScheduleITDbContext>((sp, options) =>
            {
                var connStr = sp.GetRequiredService<ConnectionString>();
                options.UseSqlServer(connStr);
            });

            services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<ScheduleITDbContext>());

            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ScheduleITDbContext>());

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();

            return services;
        }
    }
}
