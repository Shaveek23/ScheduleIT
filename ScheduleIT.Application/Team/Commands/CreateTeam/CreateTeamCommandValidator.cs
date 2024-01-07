

using FluentValidation;
using ScheduleIt.Application.Core.Errors;
using ScheduleIt.Application.Core.Extensions;

namespace ScheduleIT.Application.Team.Commands.CreateTeam
{
    /// <summary>
    /// Represents the <see cref="CreateTeamCommand"/> validator.
    /// </summary>
    public sealed class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTeamCommandValidator"/> class.
        /// </summary>
        public CreateTeamCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithError(ValidationErrors.CreateTeam.NameIsRequired);

        }
    }
}
