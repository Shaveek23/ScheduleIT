using ScheduleIT.Domain.Core.Errors;
using ScheduleIT.Domain.Core.Exceptions;
using ScheduleIT.Domain.Core.Primitives;


namespace ScheduleIT.Domain.Aggregates.Project
{
    public class TaskStatus : Enumeration<TaskStatus>
    {
        public static TaskStatus ToDo = new TaskStatus(1, nameof(ToDo).ToLowerInvariant());
        public static TaskStatus InProgress = new TaskStatus(2, nameof(InProgress).ToLowerInvariant());
        public static TaskStatus Done = new TaskStatus(3, nameof(Done).ToLowerInvariant());
        public static TaskStatus InReview = new TaskStatus(4, nameof(InReview).ToLowerInvariant());
        public static TaskStatus Cancelled = new TaskStatus(5, nameof(Cancelled).ToLowerInvariant());

        public TaskStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<TaskStatus> List() =>
            new[] { ToDo, InProgress, Done, InReview, Cancelled };

        public static TaskStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new DomainException(DomainErrors.TaskStatus.NotRecognized);
            }

            return state;
        }

        public static TaskStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Value == id);

            if (state == null)
            {
                throw new DomainException(DomainErrors.TaskStatus.NotRecognized);
            }

            return state;
        }
    }
}
