
using ScheduleIT.Domain.Aggregates.Shared;
using System.Linq.Expressions;
using ScheduleIT.Persistence.Repositories.Base;
using ScheduleIT.Domain.Aggregates.Team;



namespace ScheduleIT.Persistence.Repositories.TeamRepo
{
    /// <summary>
    /// Represents the specification for determining the team with a given employee
    /// </summary>
    internal sealed class TeamWithNameSpecification : Specification<Team>
    {
        private readonly NonEmptyText _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeWithEmailSpecification"/> class.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        internal TeamWithNameSpecification(NonEmptyText name) => _name = name;

        /// <inheritdoc />
        internal override Expression<Func<Team, bool>> ToExpression() => team => team.Name.Value == _name.Value;
    }
}
