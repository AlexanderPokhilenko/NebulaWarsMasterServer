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
        public DbSet<Warship> AccountWarships { get; set; }
        public DbSet<WarshipType> WarshipTypes { get; set; }
        public DbSet<Event> Events{ get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<FinishedMatch> FinishedMatches { get; set; }
        public DbSet<AccountInMatch> AccountsInMatches{ get; set; }
        public DbSet<RunningMatch> RunningMatches{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WarshipsConfiguration());
            modelBuilder.ApplyConfiguration(new MatchResultsConfiguration());
        }
    }
}