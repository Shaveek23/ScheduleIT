

using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace ScheduleIT.Infrastructure.BackgroundJobs.Configuration
{
    public static class QuartzJobConfigurations
    {
        public static IServiceCollection ConfigureQuartzJobs(this IServiceCollection services)
        {
            services.ConfigureProcessOutboxMessageJob();

            services.AddQuartzHostedService();

            return services;
        }

        public static IServiceCollection ConfigureProcessOutboxMessageJob(this IServiceCollection services)
        {
            services.AddQuartz(configure =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessageJob));

                configure
                    .AddJob<ProcessOutboxMessageJob>(jobKey)
                    .AddTrigger(
                        trigger =>
                            trigger.ForJob(jobKey)
                                .WithSimpleSchedule(
                                    schedule =>
                                        schedule.WithIntervalInSeconds(10)
                                            .RepeatForever()));

            });

            return services;
        }
    }
}
