

using ScheduleIT.Domain.Aggregates.Shared;

namespace ScheduleIT.Domain.Aggregates.Team
{
    public interface ITeamRepository
    {
        void Insert(Team team);
        Task<bool> IsAlreadyTeamWithEmployee(Guid employeeId);

        Task<bool> IsAlreadyTeamWithName(NonEmptyText name);
    }
}
