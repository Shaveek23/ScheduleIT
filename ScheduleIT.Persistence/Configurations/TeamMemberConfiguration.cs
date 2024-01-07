using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleIT.Domain.Aggregates.Employee;
using ScheduleIT.Domain.Aggregates.Shared;
using ScheduleIT.Domain.Aggregates.Team;

namespace ScheduleIT.Persistence.Configurations
{
    /// <summary>
    /// Represents the configuration for the <see cref="TeamMember"/> entity.
    /// </summary>
    internal sealed class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.HasKey(member => member.Id);

            builder.OwnsOne(member => member.Role, roleBuilder =>
            {
                roleBuilder.WithOwner();

                roleBuilder.Property(role => role.Value)
                    .HasColumnName(nameof(TeamMember.Role))
                    .IsRequired();
            });
   
            builder.Property(member => member.CreatedOnUtc).IsRequired();

            builder.Property(member => member.ModifiedOnUtc);

            builder.Property(member => member.DeletedOnUtc);

            builder.Property(member => member.Deleted).HasDefaultValue(false);

            builder.HasQueryFilter(member => !member.Deleted);

            builder.HasOne<Employee>()
                .WithOne()
                .HasForeignKey<TeamMember>(member => member.EmployeeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
