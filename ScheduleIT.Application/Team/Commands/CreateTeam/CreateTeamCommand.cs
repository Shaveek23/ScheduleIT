using ScheduleIt.Application.Core.Abstractions.Messaging;
using ScheduleIT.Domain.Core.Primitives.Result;


namespace ScheduleIT.Application.Team.Commands.CreateTeam
{
    public class CreateTeamCommand : ICommand<Result>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTeamResponse"/> class.
        /// </summary>
        /// <param name="name">The new team name.</param>
        /// <param name="description">The new team description.</param>
        public CreateTeamCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
