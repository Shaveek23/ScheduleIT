

using ScheduleIT.Domain.Aggregates.Shared;
using ScheduleIT.Domain.Core.Primitives.Maybe;

namespace ScheduleIT.Domain.Aggregates.Employee
{
    public interface IEmployeeRepository
    {
        Task<Maybe<Employee>> GetByEmailAsync(Email value);
        void Insert(Employee employee);
        Task<bool> IsEmailUniqueAsync(Email value);
    }
}
