using ScheduleIt.Application.Core.Abstractions.Messaging;
using ScheduleIt.Application.Core.Abstractions.Persistance;
using ScheduleIT.Application.Core.Abstractions.Authentication;
using ScheduleIT.Application.Core.Abstractions.Cryptography;
using ScheduleIT.Contracts.Authentication;
using ScheduleIT.Domain.Aggregates.Employee;
using ScheduleIT.Domain.Aggregates.Shared;
using ScheduleIT.Domain.Core.Errors;
using ScheduleIT.Domain.Core.Primitives.Result;


namespace ScheduleIt.Application.Employees.Commands.CreateEmployee
{
    /// <summary>
    /// Represents the <see cref="CreateEmployeeCommand.CreateEmployeeCommand"/> handler.
    /// </summary>
    internal sealed class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, Result<TokenResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEmployeeCommandHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="passwordHasher">The password hasher.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="jwtProvider">The JWT provider.</param>
        public CreateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork,
            IJwtProvider jwtProvider)
        {
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }

        /// <inheritdoc />
        public async Task<Result<TokenResponse>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
            Result<LastName> lastNameResult = LastName.Create(request.LastName);
            Result<Email> emailResult = Email.Create(request.Email);
            Result<Password> passwordResult = Password.Create(request.Password);

            Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(firstNameResult, lastNameResult, emailResult, passwordResult);

            if (firstFailureOrSuccess.IsFailure)
            {
                return Result.Failure<TokenResponse>(firstFailureOrSuccess.Error);
            }

            if (!await _employeeRepository.IsEmailUniqueAsync(emailResult.Value))
            {
                return Result.Failure<TokenResponse>(DomainErrors.Employee.DuplicateEmail);
            }

            string passwordHash = _passwordHasher.HashPassword(passwordResult.Value);

            var employee = Employee.Create(firstNameResult.Value, lastNameResult.Value, emailResult.Value, passwordHash);

            _employeeRepository.Insert(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            string token = _jwtProvider.Create(employee);

            return Result.Success(new TokenResponse(token));
        }
    }
}
