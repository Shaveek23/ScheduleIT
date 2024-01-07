

namespace ScheduleIT.Application.Core.Abstractions.Authentication
{
    /// <summary>
    /// Represents the employee identifier provider interface.
    /// </summary>
    public interface IEmployeeIdentifierProvider
    {
        /// <summary>
        /// Gets the authenticated employee identifier.
        /// </summary>
        Guid EmployeeId { get; }
    }
}
