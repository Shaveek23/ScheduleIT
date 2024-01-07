

using ScheduleIT.Domain.Aggregates.Employee;

namespace ScheduleIT.Application.Core.Abstractions.Authentication
{
    /// <summary>
    /// Represents the JWT provider interface.
    /// </summary>
    public interface IJwtProvider
    {
        /// <summary>
        /// Creates the JWT for the specified employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>The JWT for the specified .</returns>
        string Create(Employee employee);
    }
}
