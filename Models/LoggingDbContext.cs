using Microsoft.EntityFrameworkCore;

namespace LogApi.Models
{
    public class LoggingDbContext : DbContext
    {
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<LogInfo> LogInfos {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<LogInfo>().ToTable("LogInfo");

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
        }
    }
}