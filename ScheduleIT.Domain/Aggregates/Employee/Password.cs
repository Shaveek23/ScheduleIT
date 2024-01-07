﻿using ScheduleIT.Domain.Core.Errors;
using ScheduleIT.Domain.Core.Primitives.Result;
using ScheduleIT.Domain.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleIT.Domain.Aggregates.Employee
{

    /// <summary>
    /// Represents the password value object.
    /// </summary>
    public sealed class Password : ValueObject
    {
        private const int MinPasswordLength = 6;
        private static readonly Func<char, bool> IsLower = c => c >= 'a' && c <= 'z';
        private static readonly Func<char, bool> IsUpper = c => c >= 'A' && c <= 'Z';
        private static readonly Func<char, bool> IsDigit = c => c >= '0' && c <= '9';
        private static readonly Func<char, bool> IsNonAlphaNumeric = c => !(IsLower(c) || IsUpper(c) || IsDigit(c));

        /// <summary>
        /// Initializes a new instance of the <see cref="Password"/> class.
        /// </summary>
        /// <param name="value">The password value.</param>
        private Password(string value) => Value = value;

        /// <summary>
        /// Gets the password value.
        /// </summary>
        public string Value { get; }

        public static implicit operator string(Password password) => password?.Value ?? string.Empty;

        /// <summary>
        /// Creates a new <see cref="Password"/> instance based on the specified value.
        /// </summary>
        /// <param name="password">The password value.</param>
        /// <returns>The result of the password creation process containing the password or an error.</returns>
        public static Result<Password> Create(string password) =>
            Result.Create(password, DomainErrors.EmployeePassword.NullOrEmpty)
                .Ensure(p => !string.IsNullOrWhiteSpace(p), DomainErrors.EmployeePassword.NullOrEmpty)
                .Ensure(p => p.Length >= MinPasswordLength, DomainErrors.EmployeePassword.TooShort)
                .Ensure(p => p.Any(IsLower), DomainErrors.EmployeePassword.MissingLowercaseLetter)
                .Ensure(p => p.Any(IsUpper), DomainErrors.EmployeePassword.MissingUppercaseLetter)
                .Ensure(p => p.Any(IsDigit), DomainErrors.EmployeePassword.MissingDigit)
                .Ensure(p => p.Any(IsNonAlphaNumeric), DomainErrors.EmployeePassword.MissingNonAlphaNumeric)
                .Map(p => new Password(p));

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
    
}
