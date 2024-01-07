
using ScheduleIT.Domain.Core.Errors;
using ScheduleIT.Domain.Core.Exceptions;
using ScheduleIT.Domain.Core.Primitives;

namespace ScheduleIT.Domain.Aggregates.Team
{
    public class TeamMemberRole : Enumeration<TeamMemberRole>
    {

        public static TeamMemberRole TeamLeader = new TeamMemberRole(1, nameof(TeamLeader).ToLowerInvariant());
        public static TeamMemberRole SoftwareEngineer = new TeamMemberRole(2, nameof(SoftwareEngineer).ToLowerInvariant());
        public static TeamMemberRole Analytic = new TeamMemberRole(3, nameof(Analytic).ToLowerInvariant());
        public static TeamMemberRole Tester = new TeamMemberRole(4, nameof(Tester).ToLowerInvariant());


        public TeamMemberRole(int value, string name)
            : base(value, name)
        {
        }

        public static IEnumerable<TeamMemberRole> List() =>
            new[] { TeamLeader, SoftwareEngineer, Analytic, Tester };

        public static TeamMemberRole FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new DomainException(DomainErrors.TeamMemberRole.NotRecognized);
            }

            return state;
        }

        public static TeamMemberRole From(int id)
        {
            var state = List().SingleOrDefault(s => s.Value == id);

            if (state == null)
            {
                throw new DomainException(DomainErrors.TeamMemberRole.NotRecognized);
            }

            return state;
        }
    }
}
