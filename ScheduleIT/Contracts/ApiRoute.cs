namespace ScheduleIT.Contracts
{

    /// <summary>
    /// Contains the API endpoint routes.
    /// </summary>
    public static class ApiRoutes
    {

        public static class Authentication
        {
            public const string Login = "authentication/login";

            public const string Register = "authentication/register";
        }

        /// <summary>
        /// Contains the team routes.
        /// </summary>
        public static class Team
        {
            public const string CreateTeam = "team/create";
        }

    }
}
