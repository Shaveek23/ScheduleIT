using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using ScheduleIt.Application.Core.Abstractions.Persistance;
using ScheduleIt.Application.Employees.Commands.CreateEmployee;
using ScheduleIT.Application.Core.Abstractions.Authentication;
using ScheduleIT.Application.Core.Abstractions.Cryptography;
using ScheduleIT.Contracts.Authentication;
using ScheduleIT.Domain.Aggregates.Employee;
using ScheduleIT.Domain.Aggregates.Shared;
using ScheduleIT.Domain.Core.Errors;
using ScheduleIT.Domain.Core.Primitives.Result;


namespace ScheduleIT.Application.UnitTests.Employees.Commands
{
    public class CreateEmployeeCommandHandlerTests
    {

        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<IJwtProvider> _jwtProviderMock;

        public CreateEmployeeCommandHandlerTests()
        {
            _unitOfWorkMock = new();
            _employeeRepositoryMock = new();
            _passwordHasherMock = new();
            _jwtProviderMock = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailureResult_WhenEmailIsNotUnique()
        {
            // Arrange
            _employeeRepositoryMock.Setup(
                    e => e.IsEmailUniqueAsync(
                        It.IsAny<Email>()))
                .ReturnsAsync(false);

            var command = new CreateEmployeeCommand("FirstName", "SecondName", "mail1@test.com", "P4ssword!");

            var handler = new CreateEmployeeCommandHandler(
                _employeeRepositoryMock.Object,
                _passwordHasherMock.Object,
                _unitOfWorkMock.Object,
                _jwtProviderMock.Object
              );

            // Act
            Result<TokenResponse> res = await handler.Handle(command, default);

            // Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().Be(DomainErrors.Employee.DuplicateEmail);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult_WhenEmailIsUnique()
        {
            // Arrange
            _employeeRepositoryMock.Setup(
                    e => e.IsEmailUniqueAsync(
                        It.IsAny<Email>()))
                .ReturnsAsync(true);

            var command = new CreateEmployeeCommand("FirstName", "SecondName", "mail1@test.com", "P4ssword!");

            var handler = new CreateEmployeeCommandHandler(
                _employeeRepositoryMock.Object,
                _passwordHasherMock.Object,
                _unitOfWorkMock.Object,
                _jwtProviderMock.Object
              );

            // Act
            Result<TokenResponse> res = await handler.Handle(command, default);

            // Assert
            res.IsFailure.Should().BeFalse();
            res.Value.Should().NotBeNull();

        }
    }
}
