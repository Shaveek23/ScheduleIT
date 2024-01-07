

using ScheduleIt.Application.Core.Abstractions.Messaging;
using ScheduleIt.Application.Core.Abstractions.Persistance;
using ScheduleIT.Application.Core.Abstractions.Authentication;
using ScheduleIT.Domain.Aggregates.Shared;
using ScheduleIT.Domain.Aggregates.Team;
using ScheduleIT.Domain.Core.Errors;
using ScheduleIT.Domain.Core.Primitives.Result;

namespace ScheduleIT.Application.Team.Commands.CreateTeam
{
    internal sealed class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommand, Result>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IEmployeeIdentifierProvider _identityProvider;
        private readonly IUnitOfWork _unitOfWork;
        public CreateTeamCommandHandler(
            ITeamRepository teamRepository,
            IUnitOfWork unitOfWork,
            IEmployeeIdentifierProvider identityProvider)
        {
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
            _identityProvider = identityProvider;
        }

        public async Task<Result> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
        {
            var name = NonEmptyText.Create(command.Name);
            var description = command.Description;

            if (name.IsFailure) return Result.Failure(DomainErrors.Team.TeamNameNullOrEmpty);

            var creatorId = _identityProvider.EmployeeId;

            var isAlreadyInATeam = await _teamRepository.IsAlreadyTeamWithEmployee(creatorId);

            if (isAlreadyInATeam) return Result.Failure(DomainErrors.Team.TeamCreatorAlreadyInATeam);

            var isTeamNameTaken = await _teamRepository.IsAlreadyTeamWithName(name.Value);

            if (isTeamNameTaken) return Result.Failure(DomainErrors.Team.TeamNameDuplicate);

            var team = ScheduleIT.Domain.Aggregates.Team.Team.Create(name.Value, description, creatorId);

            _teamRepository.Insert(team);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
           
        }

    }
}
