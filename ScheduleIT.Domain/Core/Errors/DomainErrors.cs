

using ScheduleIT.Domain.Aggregates.Employee;
using ScheduleIT.Domain.Core.Primitives;

namespace ScheduleIT.Domain.Core.Errors
{
    /// <summary>
    /// Contains the domain errors.
    /// </summary>
    public static class DomainErrors
    {

        /// <summary>
        /// Contains general errors.
        /// </summary>
        public static class General
        {
            public static Error UnProcessableRequest => new Error(
                "General.UnProcessableRequest",
                "The server could not process the request.");

            public static Error ServerError => new Error("General.ServerError", "The server encountered an unrecoverable error.");
        }

        /// <summary>
        /// Contains the authentication errors.
        /// </summary>
        public static class Authentication
        {
            public static Error InvalidEmailOrPassword => new Error(
                "Authentication.InvalidEmailOrPassword",
                "The specified email or password are incorrect.");
        }

        public static class TaskStatus
        {
            public static Error NotRecognized =>
                new(
                    "TaskStatus.NotRecognized",
                    "The provided task status with the specified name was not recognized."
                        + $" Possible values for TaskStatus: "
                        + String.Join(",", Aggregates.Project.TaskStatus.List().Select(s => s.Name))
                    );
        }


        public static class TeamMemberRole
        {
            public static Error NotRecognized =>
                 new(
                    "TeamMemberRole.NotRecognized",
                    "The provided team member role with the specified name was not recognized."
                        + $" Possible values for TeamMemberRole: "
                        + String.Join(",", Aggregates.Team.TeamMemberRole.List().Select(s => s.Name))
                    );
        }

        public static class Team
        {
            public static Error NotATeamLeader =>
                new(
                    "Team.NotATeamLeader",
                    "You are not a team leader."
                );

            public static Error TeamMemberAlreadyExists =>
                new(
                    "Team.TeamMemberAlreadyExists",
                    "A member with given EmployeeId already exists in the team."
                );

            public static Error TeamMemberNotFoundOrDeleted =>
                new(
                    "Team.TeamMemberNotFoundOrDeleted",
                    "A team member with given Id not found or has been already deleted from the team."
                );

            public static Error CannotChangeRoleToTeamLeader =>
                new(
                    "Team.CannotChangeRoleToTeamLeader",
                    "A role cannot be changed to team leader without specifying their new role first."
                );

            public static Error CannotDeleteTeamMember =>
             new(
                    "Team.CannotDeleteTeamMember",
                    "Cannot remove the team leader from the team. Change the team leader first."
                );


            public static Error TeamNameNullOrEmpty =>
            new(
                "Team.TeamNameNullOrEmpty",
                "Team Name must have a value."
            );

            public static Error TeamCreatorAlreadyInATeam =>
            new(
                "Team.TeamCreatorAlreadyInATeam",
                "An employee already assigned to a team cannot create a new team."
            );

            public static Error TeamNameDuplicate =>
            new(
                "Team.TeamNameDuplicate",
                "An given team name is already taken."
            );

        }

        public static class TeamMember
        {
            public static Error TeamLeaderAlredyExists =>
                new(
                    "TeamMember.TeamLeaderAlredyExists",
                    "Cannot change role to TeamMemberRole.TeamLeader"
                );
        }

        public static class Employee
        {
            public static Error DuplicateEmail =>
                new(
                    "Employee.DuplicateEmail",
                    "Employee with a given email already exists."
                );
        }

        public static class EmployeeFirstName
        {
            public static Error NullOrEmpty =>
                new(
                    "EmployeeFirstName.NullOrEmpty",
                    "Employee first name first name cannot be empty or null."
                );

            public static Error LongerThanAllowed =>
                new(
                    "EmployeeFirstName.LongerThanAllowed",
                    $"Employee first name must be no longer than {Aggregates.Employee.FirstName.MaxLength}"
                );
        }

        public static class EmployeePassword
        {
            public static Error NullOrEmpty => new Error("Password.NullOrEmpty", "The password is required.");

            public static Error TooShort => new Error("Password.TooShort", "The password is too short.");

            public static Error MissingUppercaseLetter => new Error(
                "Password.MissingUppercaseLetter",
                "The password requires at least one uppercase letter.");

            public static Error MissingLowercaseLetter => new Error(
                "Password.MissingLowercaseLetter",
                "The password requires at least one lowercase letter.");

            public static Error MissingDigit => new Error(
                "Password.MissingDigit",
                "The password requires at least one digit.");

            public static Error MissingNonAlphaNumeric => new Error(
                "Password.MissingNonAlphaNumeric",
                "The password requires at least one non-alphanumeric.");
        }


        public static class EmployeeLastName
        {
            public static Error NullOrEmpty =>
                new(
                    "EmployeeLastName.NullOrEmpty",
                    "Employee last name first name cannot be empty or null."
                );

            public static Error LongerThanAllowed =>
                new(
                    "EmployeeLastName.LongerThanAllowed",
                    $"Employee last name must be no longer than {Aggregates.Employee.LastName.MaxLength}"
                );
        }

        public static class Shared
        {
            public static class NonEmptyText
            {
                public static Error NullOrEmpty =>
                    new(
                        "Shared.NonEmptyText.NullOrEmpty",
                        "Text valuec cannot be null or empty."
                    );
            }

            public static class Email
            {
                public static Error NullOrEmpty =>
                    new(
                        "Shared.Email.NullOrEmpty",
                        "Email address cannot be null or empty."
                    );

                public static Error LongerThanAllowed =>
                    new(
                        "Shared.Email.LongerThanAllowed",
                        $"Email address cannot be lonher than {Aggregates.Shared.Email.MaxLength}"
                    );

                public static Error InvalidFormat =>
                    new(
                        "Shared.Email.InvalidFormat",
                        $"Invalid Email Value format"
                    );
            }
        }
    }
}
