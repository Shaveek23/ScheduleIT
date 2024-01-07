using ScheduleIT.Domain.Aggregates.Employee;
using ScheduleIT.Domain.Aggregates.Shared;
using ScheduleIT.Domain.Core.Primitives.Maybe;
using ScheduleIT.Persistence.Repositories.Base;

namespace ScheduleIT.Persistence.Repositories.EmployeeRepo
{
    internal class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ScheduleITDbContext dbContext)
            : base(dbContext)
        {

        }

        /// <inheritdoc />
        public async Task<Maybe<Employee>> GetByEmailAsync(Email email) => await FirstOrDefaultAsync(new UserWithEmailSpecification(email));

        /// <inheritdoc />
        public async Task<bool> IsEmailUniqueAsync(Email email) => !await AnyAsync(new UserWithEmailSpecification(email));

    }
}
