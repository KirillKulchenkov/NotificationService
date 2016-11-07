using System.Data.Entity.ModelConfiguration;

namespace NotificationService.Domain.Models.Mapping
{
    public class ExpiredTokenMap : EntityTypeConfiguration<ExpiredToken>
    {
        public ExpiredTokenMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ExpiredToken");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Expired).HasColumnName("Expired");
            this.Property(t => t.Token).HasColumnName("Token");
            this.Property(t => t.NewToken).HasColumnName("NewToken");
        }
    }
}