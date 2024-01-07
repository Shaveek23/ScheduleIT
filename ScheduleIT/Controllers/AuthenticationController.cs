using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleIt.Application.Employees.Commands.CreateEmployee;
using ScheduleIT.Application.Authentication.Commands.Login;
using ScheduleIT.Contracts;
using ScheduleIT.Contracts.Authentication;
using ScheduleIT.Controllers.Base;
using ScheduleIT.Domain.Core.Errors;
using ScheduleIT.Domain.Core.Primitives.Result;
using LoginRequest = ScheduleIT.Contracts.Authentication.LoginRequest;
using RegisterRequest = ScheduleIT.Contracts.Authentication.RegisterRequest;

namespace ScheduleIT.Controllers
{
    namespace EventReminder.Services.Api.Controllers
    {
        [AllowAnonymous]
        public sealed class AuthenticationController : ApiController
        {
            public AuthenticationController(IMediator mediator)
                : base(mediator)
            {
            }

            [HttpPost(ApiRoutes.Authentication.Login)]
            [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
            public async Task<IActionResult> Login(LoginRequest loginRequest) =>
                await Result.Create(loginRequest, DomainErrors.General.UnProcessableRequest)
                    .Map(request => new LoginCommand(request.Email, request.Password))
                    .Bind(command => Mediator.Send(command))
                    .Match(Ok, BadRequest);

            [AllowAnonymous]
            [HttpPost(ApiRoutes.Authentication.Register)]
            [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
            public async Task<IActionResult> Create(RegisterRequest registerRequest) =>
                await Result.Create(registerRequest, DomainErrors.General.UnProcessableRequest)
                    .Map(request => new CreateEmployeeCommand(request.FirstName, request.LastName, request.Email, request.Password))
                    .Bind(command => Mediator.Send(command))
                    .Match(Ok, BadRequest);
        }
    }
}
