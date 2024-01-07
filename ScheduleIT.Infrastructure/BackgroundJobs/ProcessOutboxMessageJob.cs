

using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using ScheduleIT.Domain.Core.Primitives.Events;
using ScheduleIT.Infrastructure.Common;
using ScheduleIT.Persistence;

namespace ScheduleIT.Infrastructure.BackgroundJobs
{

    [DisallowConcurrentExecution]
    public class ProcessOutboxMessageJob : IJob
    {
        private readonly ScheduleITDbContext _dbContext;
        private readonly IMediator _publisher;
        private readonly int _batchSize = 20;

        public ProcessOutboxMessageJob(ScheduleITDbContext dbContext, IMediator publisher)
        {
            _dbContext = dbContext;
            _publisher = publisher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
                await ProcessPersistedOutboxMessages(context);
        }

        private async Task ProcessPersistedOutboxMessages(IJobExecutionContext context)
        {
            var messages = await _dbContext
                .SetOutboxMessages()
                .Where(x => x.ProcessedOnUtc == null)
                .Take(_batchSize)
                .ToListAsync(context.CancellationToken);

            foreach (var outboxMessage in messages)
            {
                try
                {
                    IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                        outboxMessage.Content,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Objects
                        });

                    if (domainEvent is null)
                    {
                        // TO DO: log error
                        continue;
                    }

                    await _publisher.Publish(domainEvent, context.CancellationToken);

                    outboxMessage.ProcessedOnUtc = DateTime.UtcNow;

                    await _dbContext.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    // TO DO: log error
                    continue;
                }


            }

           
        }
    }
}
