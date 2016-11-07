using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using NotificationService.Domain.Models;
using NotificationService.Domain.Models.Mapping;

namespace NotificationService.Domain
{
    public partial class DatabaseContext : DbContext
    {
        static DatabaseContext()
        {
            Database.SetInitializer<DatabaseContext>(null);
        }

        public DatabaseContext()
            : base("Name=DatabaseContext")
        {
        }

        public DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public DbSet<MobileAppInfo> MobileAppInfoes { get; set; }
        public DbSet<QueueLog> QueueLogs { get; set; }
        public DbSet<ExpiredToken> ExpiredTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new C__RefactorLogMap());
            modelBuilder.Configurations.Add(new MobileAppInfoMap());
            modelBuilder.Configurations.Add(new QueueLogMap());
            modelBuilder.Configurations.Add(new ExpiredTokenMap());
        }
    }
}
