using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntegrationEventLogEF
{
    public class IntegrationEventLogContext : DbContext
    {
        public IntegrationEventLogContext(DbContextOptions<IntegrationEventLogContext> options) : base(options)
        {
        }

        public DbSet<IntegrationEventLogEntry> IntegrationEventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IntegrationEventLogEntry>(ConfigureIntegrationEventLogEntry);
        }

        void ConfigureIntegrationEventLogEntry(EntityTypeBuilder<IntegrationEventLogEntry> builder)
        {
            builder.ToTable("integration_event_log");

            builder.HasKey(e => e.EventId);

            builder.Property(e => e.EventId).HasColumnName("event_id")
                .IsRequired();

            builder.Property(e => e.Content).HasColumnName("content")
                .IsRequired();

            builder.Property(e => e.CreationTime).HasColumnName("creation_time")
                .IsRequired();

            builder.Property(e => e.State).HasColumnName("state")
                .IsRequired();

            builder.Property(e => e.TimesSent).HasColumnName("times_sent")
                .IsRequired();

            builder.Property(e => e.EventTypeName).HasColumnName("event_type_name")
                .IsRequired();

        }
    }
}
