
using ScheduleIT.Domain.Core.Errors;
using ScheduleIT.Domain.Core.Primitives.Result;
using ScheduleIT.Domain.Core.Primitives;

namespace ScheduleIT.Domain.Aggregates.Shared
{
    /// <summary>
    /// Represents the non empty text value object.
    /// </summary>
    public sealed class NonEmptyText : ValueObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NonEmptyText"/> class.
        /// </summary>
        /// <param name="value">The non empty text value.</param>
        private NonEmptyText(string value) => Value = value;

        /// <summary>
        /// Gets the non empty text value.
        /// </summary>
        public string Value { get; }

        public static implicit operator string(NonEmptyText firstName) => firstName.Value;

        /// <summary>
        /// Creates a new <see cref="NonEmptyText"/> instance based on the specified value.
        /// </summary>
        /// <param name="text">The non empty text value.</param>
        /// <returns>The result of the non empty text creation process containing the non empty text or an error.</returns>
        public static Result<NonEmptyText> Create(string text) =>
            Result.Create(text, DomainErrors.EmployeeFirstName.NullOrEmpty)
                .Ensure(f => !string.IsNullOrWhiteSpace(f), DomainErrors.Shared.NonEmptyText.NullOrEmpty)
                .Map(f => new NonEmptyText(f));

        /// <inheritdoc />
        public override string ToString() => Value;

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
