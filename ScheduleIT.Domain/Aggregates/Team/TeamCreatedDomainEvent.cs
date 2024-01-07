using ScheduleIT.Domain.Core.Primitives.Events;

namespace ScheduleIT.Domain.Aggregates.Team
{

    /// <summary>
    /// Represents the event that is raised when a team is created.
    /// </summary>

    public sealed record TeamCreatedDomainEvent(Guid TeamId, string Name, Guid CreatedByEmployeeId, DateTime CreatedOnUtc) : IDomainEvent
    {

    }
}
