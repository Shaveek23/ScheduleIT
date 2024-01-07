using MediatR;
using ScheduleIt.Application.Core.Abstractions.Persistance;
using ScheduleIT.Application.Core.Abstractions.Messaging;
using ScheduleIT.Domain.Aggregates.Team;
using ScheduleIT.Domain.Core.Primitives.Events;


namespace ScheduleIT.Application.Team.Events.TeamCreated
{
    /// <summary>
    /// Represents the <see cref="TeamCreatedDomainEvent"/> handler.
    /// </summary>
    internal sealed class TeamCreatedDomainEventHandler : IDomainEventHandler<TeamCreatedDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamCreatedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="integrationEventPublisher">The integration event publisher.</param>
        public TeamCreatedDomainEventHandler(
            IIntegrationEventPublisher integrationEventPublisher
        )
        {
            _integrationEventPublisher = integrationEventPublisher;
        }

        /// <inheritdoc />
        public async Task Handle(TeamCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _integrationEventPublisher.Publish(new TeamCreatedIntegrationEvent(notification));

            await Task.CompletedTask;
        }
    }
}
