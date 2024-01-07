using ScheduleIT.Domain.Aggregates.Shared;
using ScheduleIT.Domain.Core.Abstractions;
using ScheduleIT.Domain.Core.Primitives;


namespace ScheduleIT.Domain.Aggregates.Project
{
    public class ProjectTask : Entity, IAuditableEntity, ISoftDeletableEntity
    {
         public NonEmptyText Name { get; private set; }

        public string? Description { get; private set; }

         public Guid? AssignedTeamMemberId { get; private set; }

         public Guid ProjectId { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; } 

        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; }

        /// <inheritdoc />
        public bool Deleted { get; }

        private ProjectTask(NonEmptyText name, string? description, Guid? assignedTeamMemberId, Guid projectId)
        {
            Name = name;
            Description = description;
            AssignedTeamMemberId = assignedTeamMemberId;
            ProjectId = projectId;
        }

         internal static ProjectTask Create(NonEmptyText name, string? description, Guid? assignedTeamMemberId, Guid projectId)
         {
            return new ProjectTask(name, description, assignedTeamMemberId, projectId);
         }

         
    }
}
