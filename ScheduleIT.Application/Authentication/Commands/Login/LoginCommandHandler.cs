using ScheduleIT.Application.Core.Abstractions.Authentication;
using ScheduleIt.Application.Core.Abstractions.Messaging;
using ScheduleIT.Contracts.Authentication;
using ScheduleIT.Domain.Core.Errors;
using ScheduleIT.Domain.Core.Primitives.Result;
using ScheduleIT.Domain.Aggregates.Employee;
using ScheduleIT.Domain.Aggregates.Shared;
using ScheduleIT.Domain.Services;
using ScheduleIT.Domain.Core.Primitives.Maybe;


namespace ScheduleIT.Application.Authentication.Commands.Login
{
    /// <summary>
    /// Represents the <see cref="LoginCommand"/> handler.
    /// </summary>
    internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, Result<TokenResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHashChecker _passwordHashChecker;
        private readonly IJwtProvider _jwtProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginCommandHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The employee repository.</param>
        /// <param name="passwordHashChecker">The password hash checker.</param>
        /// <param name="jwtProvider">The JWT provider.</param>
        public LoginCommandHandler(IEmployeeRepository userRepository, IPasswordHashChecker passwordHashChecker, IJwtProvider jwtProvider)
        {
            _employeeRepository = userRepository;
            _passwordHashChecker = passwordHashChecker;
            _jwtProvider = jwtProvider;
        }

        /// <inheritdoc />
        public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Result<Email> emailResult = Email.Create(request.Email);

            if (emailResult.IsFailure)
            {
                return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);
            }

            Maybe<Employee> maybeUser = await _employeeRepository.GetByEmailAsync(emailResult.Value);

            if (maybeUser.HasNoValue)
            {
                return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);
            }

            Employee employee = maybeUser.Value;

            bool passwordValid = employee.VerifyPasswordHash(request.Password, _passwordHashChecker);

            if (!passwordValid)
            {
                return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);
            }

            string token = _jwtProvider.Create(employee);

            return Result.Success(new TokenResponse(token));
        }
    }
}
