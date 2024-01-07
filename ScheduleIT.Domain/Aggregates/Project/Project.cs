using ScheduleIT.Domain.Aggregates.Shared;
using ScheduleIT.Domain.Core.Abstractions;
using ScheduleIT.Domain.Core.Primitives;
using ScheduleIT.Domain.Core.Primitives.Result;


namespace ScheduleIT.Domain.Aggregates.Project
{
    public class Project : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
    {

        private readonly List<ProjectTask> _projectTasks = [];

        public NonEmptyText Name { get; private set; }

        public string Description { get; private set; } = string.Empty;

        public IReadOnlyCollection<ProjectTask> ProjectTasks => _projectTasks;

        public Guid? AssignedTeamId { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; }

        /// <inheritdoc />
        public bool Deleted { get; }


        private Project(NonEmptyText name, string description) : base(Guid.NewGuid())
        {
            Name = name;
            Description = description;
            CreatedOnUtc = DateTime.UtcNow;
            
        }
        public static Project CreateProject(NonEmptyText name, string description)
        {
            return new Project(name, description);
        }

        public Result CreateNewTask(NonEmptyText taskName, string? taskDescription, Guid? teamMemberId)
        {
            var task = ProjectTask.Create(taskName, taskDescription, teamMemberId, this.Id);

            _projectTasks.Add(task);

            return Result.Success();
        }

        public Result AssignTeam(Guid teamId)
        {
            this.AssignedTeamId = teamId;

            return Result.Success();
        }

        public Result ChangeName(NonEmptyText newName)
        {
            this.Name = newName;

            return Result.Success();
        }

        public Result ChangeDescription(string newDescription)
        {
            this.Description = newDescription;

            return Result.Success();
        }
    }
}
