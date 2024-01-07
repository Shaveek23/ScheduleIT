
using ScheduleIT.Domain.Aggregates.Shared;
using System.Linq.Expressions;
using ScheduleIT.Persistence.Repositories.Base;
using ScheduleIT.Domain.Aggregates.Employee;



namespace ScheduleIT.Persistence.Repositories.EmployeeRepo
{
    /// <summary>
    /// Represents the specification for determining the employee with email.
    /// </summary>
    internal sealed class UserWithEmailSpecification : Specification<Employee>
    {
        private readonly Email _email;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeWithEmailSpecification"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        internal UserWithEmailSpecification(Email email) => _email = email;

        /// <inheritdoc />
        internal override Expression<Func<Employee, bool>> ToExpression() => employee => employee.Email.Value == _email;
    }
}
