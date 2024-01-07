using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScheduleIT.Application.Team.Commands.CreateTeam;
using ScheduleIT.Contracts;
using ScheduleIT.Contracts.Team.CreateTeam;
using ScheduleIT.Controllers.Base;
using ScheduleIT.Domain.Core.Errors;
using ScheduleIT.Domain.Core.Primitives.Result;

namespace ScheduleIT.Controllers
{
    public class TeamController : ApiController
    {
        public TeamController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost(ApiRoutes.Team.CreateTeam)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTeam(CreateTeamRequest createTeamRequest) =>
            await Result.Create(createTeamRequest, DomainErrors.General.UnProcessableRequest)
                .Map(r => new CreateTeamCommand(r.Name, r.Description))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        
    }
}
