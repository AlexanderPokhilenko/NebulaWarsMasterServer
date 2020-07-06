using DataLayer.Tables;
using DataLayer.TablesConfiguration;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Match> Matches { get; set; }
        public DbSet<GameModeType> GameModeTypes { get; set; }
        public DbSet<MatchResult> MatchResults { get; set; }
        public DbSet<WarshipCombatRole> WarshipCombatRoles { get; set; }
        
        
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        
        
        
        public DbSet<Increment> Increments { get; set; }
        public DbSet<IncrementType> IncrementTypes { get; set; }
        public DbSet<MatchRewardType> MatchRewardTypes { get; set; }
        
        
        public DbSet<Decrement> Decrements { get; set; }
        public DbSet<DecrementType> DecrementTypes { get; set; }
        
        
        public DbSet<ShopModelDb> ShopModels { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountsConfiguration());
            modelBuilder.ApplyConfiguration(new WarshipsConfiguration());
            modelBuilder.ApplyConfiguration(new MatchResultsConfiguration());
            modelBuilder.ApplyConfiguration(new WarshipTypesConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionsConfiguration());
            modelBuilder.ApplyConfiguration(new DecrementsConfiguration());
            modelBuilder.ApplyConfiguration(new IncrementsConfiguration());
            modelBuilder.ApplyConfiguration(new ShopModelDbConfiguration());
        }
    }
}