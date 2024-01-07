
using ScheduleIT.Domain.Aggregates.Shared;
using System.Linq.Expressions;
using ScheduleIT.Persistence.Repositories.Base;
using ScheduleIT.Domain.Aggregates.Team;



namespace ScheduleIT.Persistence.Repositories.TeamRepo
{
    /// <summary>
    /// Represents the specification for determining the team with a given employee
    /// </summary>
    internal sealed class TeamWithEmployeeSpecification : Specification<Team>
    {
        private readonly Guid _employeeId;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeWithEmailSpecification"/> class.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        internal TeamWithEmployeeSpecification(Guid employeeId) => _employeeId = employeeId;

        /// <inheritdoc />
        internal override Expression<Func<Team, bool>> ToExpression() => team => team.Members.Any(m => m.EmployeeId == _employeeId);
    }
}
