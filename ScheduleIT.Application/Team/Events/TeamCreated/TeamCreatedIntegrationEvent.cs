using Newtonsoft.Json;
using ScheduleIT.Application.Core.Abstractions.Messaging;
using ScheduleIT.Domain.Aggregates.Team;


namespace ScheduleIT.Application.Team.Events.TeamCreated
{
    /// <summary>
    /// Represents the integration event that is raised when a new team is created.
    /// </summary>
    public sealed class TeamCreatedIntegrationEvent : IIntegrationEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamCreatedIntegrationEvent"/> class.
        /// </summary>
        /// <param name="teamCreatedDomainEvent">The team created domain event.</param>
        internal TeamCreatedIntegrationEvent(TeamCreatedDomainEvent teamCreatedDomainEvent) =>
            TeamId = teamCreatedDomainEvent.TeamId;
        
        [JsonConstructor]
        private TeamCreatedIntegrationEvent(Guid teamId) => TeamId = teamId;

        /// <summary>
        /// Gets the invitation identifier.
        /// </summary>
        public Guid TeamId { get; }
    }
}
