using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleIT.Persistence.Outbox;

namespace ScheduleIT.Persistence.Configurations
{
    /// <summary>
    /// Represents the configuration for the <see cref="OutboxMessage"/>.
    /// </summary>
    internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.HasKey(msg => msg.Id);

            builder.Property(msg => msg.Type)
                .IsRequired();

            builder.Property(msg => msg.OccurredOnUtc)
                .IsRequired();

            builder.Property(msg => msg.ProcessedOnUtc);

            builder.Property(msg => msg.Content)
                .IsRequired();

            builder.Property(msg => msg.Error);

            builder
                .HasIndex(x => x.ProcessedOnUtc)
                .HasDatabaseName("IX_OutboxMessage_ProcessedOnUtc");

        }
    }
}
