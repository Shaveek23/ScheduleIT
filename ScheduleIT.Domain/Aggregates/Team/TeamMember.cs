using ScheduleIT.Domain.Core.Abstractions;
using ScheduleIT.Domain.Core.Primitives;


namespace ScheduleIT.Domain.Aggregates.Team
{
    public class TeamMember : Entity, IAuditableEntity, ISoftDeletableEntity
    {
        public Guid EmployeeId { get; private set; }

        public TeamMemberRole Role { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; private set; }

        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; private set; }

        /// <inheritdoc />
        public bool Deleted { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMember"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private TeamMember()
        {
        }
        private TeamMember(Guid employeeId, TeamMemberRole role) : base(Guid.NewGuid())
        {
            EmployeeId = employeeId;
            Role = role;
            CreatedOnUtc = DateTime.UtcNow;
        }
        
        internal static TeamMember Create(Guid employeeId, TeamMemberRole role)
        {
            return new TeamMember(employeeId, role);
        }

        internal void ChangeRole(TeamMemberRole role) 
        {
            Role = role;
            ModifiedOnUtc = DateTime.UtcNow;
        }

        public void MarkAsDeleted()
        {
            if (Deleted == true)
            {
                return;
            }

            DeletedOnUtc = DateTime.UtcNow;
            Deleted = true;
        }

    }
}
