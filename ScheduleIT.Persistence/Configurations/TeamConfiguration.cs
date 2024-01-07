using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using ScheduleIT.Domain.Aggregates.Employee;
using ScheduleIT.Domain.Aggregates.Shared;
using ScheduleIT.Domain.Aggregates.Team;

namespace ScheduleIT.Persistence.Configurations
{
    /// <summary>
    /// Represents the configuration for the <see cref="Team"/> entity.
    /// </summary>
    internal sealed class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(team => team.Id);

            builder.OwnsOne(team => team.Name, firstNameBuilder =>
            {
                firstNameBuilder.WithOwner();

                firstNameBuilder.Property(name => name.Value)
                    .HasColumnName(nameof(Team.Name))
                    .IsRequired();
            });

            builder.Property(team => team.Description)
                .HasMaxLength(255);

            builder.Property(team => team.CreatedOnUtc).IsRequired();

            builder.Property(team => team.ModifiedOnUtc);

            builder.Property(team => team.DeletedOnUtc);

            builder.Property(team => team.Deleted).HasDefaultValue(false);

            builder.HasQueryFilter(team => !team.Deleted);

            builder.HasOne<Employee>()
                .WithOne() // TeamLeader can lead only one team
                .HasForeignKey<Team>(t => t.TeamLeaderEmployeeId)
                .IsRequired();


            var navigation = builder.Metadata.FindNavigation(nameof(Team.Members));

            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);


        }
    }
}
