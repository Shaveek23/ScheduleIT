

using FluentValidation;
using ScheduleIt.Application.Core.Errors;
using ScheduleIt.Application.Core.Extensions;

namespace ScheduleIt.Application.Employees.Commands.CreateEmployee
{
    /// <summary>
    /// Represents the <see cref="CreateEmployeeCommand"/> validator.
    /// </summary>
    public sealed class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEmployeeCommandValidator"/> class.
        /// </summary>
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithError(ValidationErrors.CreateEmployee.FirstNameIsRequired);

            RuleFor(x => x.LastName).NotEmpty().WithError(ValidationErrors.CreateEmployee.LastNameIsRequired);

            RuleFor(x => x.Email).NotEmpty().WithError(ValidationErrors.CreateEmployee.EmailIsRequired);

            RuleFor(x => x.Password).NotEmpty().WithError(ValidationErrors.CreateEmployee.PasswordIsRequired);
        }
    }
}
