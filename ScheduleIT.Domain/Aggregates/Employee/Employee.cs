using ScheduleIT.Domain.Aggregates.Shared;
using ScheduleIT.Domain.Core.Abstractions;
using ScheduleIT.Domain.Core.Primitives;
using ScheduleIT.Domain.Services;

namespace ScheduleIT.Domain.Aggregates.Employee
{
    public class Employee : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
    {

        private string _passwordHash;

        /// <summary>
        /// Gets the employee name.
        /// </summary>
        public FirstName FirstName { get; private set; }

        /// <summary>
        /// Gets the employee last name.
        /// </summary>
        public LastName LastName { get; private set; }

        /// <summary>
        /// Gets the employee email address.
        /// </summary>
        public Email Email { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; }

        /// <inheritdoc />
        public bool Deleted { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Employee()
        {
        }

        internal Employee(FirstName firstName, LastName lastName, Email email, string passwordHash) : base(Guid.NewGuid())
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedOnUtc = DateTime.UtcNow;
            _passwordHash = passwordHash;
        }

        public static Employee Create(FirstName firstName, LastName lastName, Email email, string passwordHash)
        {

            var employee = new Employee(firstName, lastName, email, passwordHash);

            return employee;
        }

        public bool VerifyPasswordHash(string password, IPasswordHashChecker passwordHashChecker)
        {
            return passwordHashChecker.HashesMatch(_passwordHash, password);
        }
    }
}
