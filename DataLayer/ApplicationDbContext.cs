using DataLayer.Tables;
using DataLayer.TablesConfiguration;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataLayer
{
    public sealed class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Warship> Warships { get; set; }
        public DbSet<WarshipType> WarshipTypes { get; set; }
        public DbSet<FinishedMatch> FinishedMatches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WarshipsConfiguration());
            modelBuilder.ApplyConfiguration(new MatchResultsConfiguration());
            modelBuilder.ApplyConfiguration(new WarshipTypeConfiguration());
            
            modelBuilder.Entity<Warship>()
                .HasOne(w => w.Account)
                .WithMany(a => a.Warships);
            

            modelBuilder.Entity<Account>()
                .HasIndex(account => account.ServiceId)
                .IsUnique();
            
            
            modelBuilder.Entity<Warship>()
                .HasOne(warship => warship.WarshipType)
                .WithMany(warshipType => warshipType.Warships)
                .HasForeignKey(warship => warship.WarshipTypeId);

        }
    }
}