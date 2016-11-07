using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace NotificationService.Domain.Models.Mapping
{
    public class MobileAppInfoMap : EntityTypeConfiguration<MobileAppInfo>
    {
        public MobileAppInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.MobileAppName)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.IOSCertificateFile)
                .IsRequired();

            this.Property(t => t.CertificatePassword)
                .IsRequired();

            this.Property(t => t.AndriodAuthtoken)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("MobileAppInfo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MobileAppName).HasColumnName("MobileAppName");
            this.Property(t => t.IOSCertificateFile).HasColumnName("IOSCertificateFile");
            this.Property(t => t.CertificatePassword).HasColumnName("CertificatePassword");
            this.Property(t => t.AndriodAuthtoken).HasColumnName("AndriodAuthtoken");
            this.Property(t => t.AndroidSenderId).HasColumnName("AndroidSenderId");
            this.Property(t => t.SourceId).HasColumnName("SourceId");
            this.Property(t => t.Modified).HasColumnName("Modified");
        }
    }
}
