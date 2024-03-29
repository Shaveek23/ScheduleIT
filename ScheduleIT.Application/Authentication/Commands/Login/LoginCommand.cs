﻿using ScheduleIt.Application.Core.Abstractions.Messaging;
using ScheduleIT.Contracts.Authentication;
using ScheduleIT.Domain.Core.Primitives.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleIT.Application.Authentication.Commands.Login
{
    /// <summary>
    /// Represents the login command.
    /// </summary>
    public sealed class LoginCommand : ICommand<Result<TokenResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginCommand"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public string Password { get; }
    }
}
