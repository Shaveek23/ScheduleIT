

using ScheduleIT.Domain.Core.Primitives;

namespace ScheduleIt.Application.Core.Errors
{
    /// <summary>
    /// Contains the validation errors.
    /// </summary>
    internal static class ValidationErrors
    {

        internal static class CreateEmployee
        {
            internal static Error FirstNameIsRequired => new Error("CreateEmployee.FirstNameIsRequired", "The first name is required.");

            internal static Error LastNameIsRequired => new Error("CreateEmployee.LastNameIsRequired", "The last name is required.");

            internal static Error EmailIsRequired => new Error("CreateEmployee.EmailIsRequired", "The email is required.");

            internal static Error PasswordIsRequired => new Error("CreateEmployee.PasswordIsRequired", "The password is required.");
        }


        internal static class CreateTeam
        {
            internal static Error NameIsRequired => new Error("CreateTeam.NameIsRequired", "The team name is required");
        }

    }
}
