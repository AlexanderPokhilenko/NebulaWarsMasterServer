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
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<LootboxDb> Lootbox { get; set; }
        public DbSet<WarshipCombatRole> WarshipCombatRole { get; set; }
        public DbSet<LootboxPrizeRegularCurrency> LootboxPrizeRegularCurrencies { get; set; }
        public DbSet<LootboxPrizeWarshipPowerPoints> LootboxPrizeWarshipPowerPoints { get; set; }
        public DbSet<LootboxPrizePointsForSmallLootbox> LootboxPrizePointsForSmallLootboxes { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<Kit> Kits { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WarshipImprovementPurchase> WarshipImprovementPurchases { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountsConfiguration());
            modelBuilder.ApplyConfiguration(new WarshipsConfiguration());
            modelBuilder.ApplyConfiguration(new MatchResultsConfiguration());
        }
    }
}