using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace NotificationService.Domain.Models.Mapping
{
    public class QueueLogMap : EntityTypeConfiguration<QueueLog>
    {
        public QueueLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("QueueLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.TriesDone).HasColumnName("TriesDone");
            this.Property(t => t.Sent).HasColumnName("Sent");
            this.Property(t => t.Error).HasColumnName("Error");
            this.Property(t => t.SourceId).HasColumnName("SourceId");
            Property(t => t.Payload).HasColumnName("Payload");
            Property(t => t.Token).HasColumnName("Token");
        }
    }
}
