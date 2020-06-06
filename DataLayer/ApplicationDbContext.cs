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
        public DbSet<MatchResultForPlayer> MatchResultForPlayers { get; set; }
        public DbSet<WarshipCombatRole> WarshipCombatRole { get; set; }
        public DbSet<LootboxDb> Lootbox { get; set; }
        public DbSet<LootboxPrizeSoftCurrency> LootboxPrizeSoftCurrency { get; set; }
        public DbSet<LootboxPrizeHardCurrency> LootboxPrizeHardCurrency { get; set; }
        public DbSet<LootboxPrizeWarshipPowerPoints> LootboxPrizeWarshipPowerPoints { get; set; }
        public DbSet<LootboxPrizeSmallLootboxPoints> LootboxPrizeSmallLootboxPoints { get; set; }
        
        // public DbSet<Purchase> Purchases { get; set; }
        // public DbSet<Order> Orders { get; set; }
        // public DbSet<Kit> Kits { get; set; }
        // public DbSet<Product> Products { get; set; }
        // public DbSet<WarshipImprovementPurchase> WarshipPowerPoints { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountsConfiguration());
            modelBuilder.ApplyConfiguration(new WarshipsConfiguration());
            modelBuilder.ApplyConfiguration(new MatchResultsConfiguration());
            modelBuilder.ApplyConfiguration(new WarshipTypesConfiguration());
            modelBuilder.ApplyConfiguration(new LootboxesConfiguration());
            modelBuilder.ApplyConfiguration(new LootboxPrizeSoftCurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new LootboxPrizeWarshipPowerPointsConfiguration());
            modelBuilder.ApplyConfiguration(new LootboxPrizePointsForSmallLootboxConfiguration());
            modelBuilder.ApplyConfiguration(new LootboxPrizeHardCurrencyConfiguration());
        }
    }
}