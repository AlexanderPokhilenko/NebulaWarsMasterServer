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
        public DbSet<GameMode> GameModes { get; set; }
        public DbSet<BattleRoyaleMatchResult> BattleRoyaleMatchResults { get; set; }
        public DbSet<WarshipCombatRole> WarshipCombatRoles { get; set; }
        
        
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        
        
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        
        
        public DbSet<Increment> Increments { get; set; }
        public DbSet<IncrementType> IncrementTypes { get; set; }
        
        
        public DbSet<Decrement> Decrements { get; set; }
        public DbSet<DecrementType> DecrementTypes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountsConfiguration());
            modelBuilder.ApplyConfiguration(new WarshipsConfiguration());
            modelBuilder.ApplyConfiguration(new MatchResultsConfiguration());
            modelBuilder.ApplyConfiguration(new WarshipTypesConfiguration());
            
            modelBuilder.Entity<Transaction>()
                .HasOne(order => order.Account)
                .WithMany(account => account.Transactions)
                .HasForeignKey(order => order.AccountId);
            
            modelBuilder.Entity<Transaction>()
                .HasOne(order => order.TransactionType)
                .WithMany(orderType => orderType.Orders)
                .HasForeignKey(order => order.TransactionTypeId);
            
            modelBuilder.Entity<Resource>()
                .HasOne(prod => prod.Transaction)
                .WithMany(order => order.Resources)
                .HasForeignKey(prod => prod.TransactionId);
            
            modelBuilder.Entity<Resource>()
                .HasOne(prod => prod.ResourceType)
                .WithMany(productType => productType.Products)
                .HasForeignKey(prod => prod.ResourceTypeId);
            
            modelBuilder.Entity<Increment>()
                .HasOne(inc => inc.Resource)
                .WithMany(prod => prod.Increments)
                .HasForeignKey(inc => inc.ResourceId);
            
            modelBuilder.Entity<Decrement>()
                .HasOne(decr => decr.Resource)
                .WithMany(prod => prod.Decrements)
                .HasForeignKey(decr => decr.ResourceId);
        }
    }
}