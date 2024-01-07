

using ScheduleIt.Application.Core.Abstractions.Persistance;
using ScheduleIT.Domain.Aggregates.Shared;
using ScheduleIT.Domain.Aggregates.Team;
using ScheduleIT.Persistence.Repositories.Base;

namespace ScheduleIT.Persistence.Repositories.TeamRepo
{
    internal class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(ScheduleITDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> IsAlreadyTeamWithEmployee(Guid employeeId) => await AnyAsync(new TeamWithEmployeeSpecification(employeeId));

        public async Task<bool> IsAlreadyTeamWithName(NonEmptyText name) => await AnyAsync(new TeamWithNameSpecification(name));

    }
}
