using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleIT.Domain.Aggregates.Employee;
using ScheduleIT.Domain.Aggregates.Shared;

namespace ScheduleIT.Persistence.Configurations
{
    /// <summary>
    /// Represents the configuration for the <see cref="Employee"/> entity.
    /// </summary>
    internal sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(employee => employee.Id);

            builder.OwnsOne(employee => employee.FirstName, firstNameBuilder =>
            {
                firstNameBuilder.WithOwner();

                firstNameBuilder.Property(firstName => firstName.Value)
                    .HasColumnName(nameof(Employee.FirstName))
                    .HasMaxLength(FirstName.MaxLength)
                    .IsRequired();
            });

            builder.OwnsOne(employee => employee.LastName, lastNameBuilder =>
            {
                lastNameBuilder.WithOwner();

                lastNameBuilder.Property(lastName => lastName.Value)
                    .HasColumnName(nameof(Employee.LastName))
                    .HasMaxLength(LastName.MaxLength)
                    .IsRequired();
            });

            builder.OwnsOne(employee => employee.Email, emailBuilder =>
            {
                emailBuilder.WithOwner();

                emailBuilder.Property(email => email.Value)
                    .HasColumnName(nameof(Employee.Email))
                    .HasMaxLength(Email.MaxLength)
                    .IsRequired();

                emailBuilder.HasIndex(email => email.Value)
                    .IsUnique();

            });
                

            builder.Property<string>("_passwordHash")
                .HasField("_passwordHash")
                .HasColumnName("PasswordHash")
                .IsRequired();

            builder.Property(employee => employee.CreatedOnUtc).IsRequired();

            builder.Property(employee => employee.ModifiedOnUtc);

            builder.Property(employee => employee.DeletedOnUtc);

            builder.Property(employee => employee.Deleted).HasDefaultValue(false);

            builder.HasQueryFilter(employee => !employee.Deleted);

        }
    }
}
