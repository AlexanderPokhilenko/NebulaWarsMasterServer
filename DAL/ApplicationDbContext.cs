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
        public DbSet<WarshipCombatRole> WarshipCombatRoles { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        
        
        public DbSet<Increment> Increments { get; set; }
        public DbSet<IncrementType> IncrementTypes { get; set; }
        
        
        public DbSet<Decrement> Decrement { get; set; }
        public DbSet<DecrementType> DecrementTypes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountsConfiguration());
            modelBuilder.ApplyConfiguration(new WarshipsConfiguration());
            modelBuilder.ApplyConfiguration(new MatchResultsConfiguration());
            modelBuilder.ApplyConfiguration(new WarshipTypesConfiguration());
            
            modelBuilder.Entity<Order>()
                .HasOne(order => order.Account)
                .WithMany(account => account.Orders)
                .HasForeignKey(order => order.AccountId);
            
            modelBuilder.Entity<Order>()
                .HasOne(order => order.OrderType)
                .WithMany(orderType => orderType.Orders)
                .HasForeignKey(order => order.OrderTypeId);
            
            modelBuilder.Entity<Product>()
                .HasOne(prod => prod.Order)
                .WithMany(order => order.Products)
                .HasForeignKey(prod => prod.OrderId);
            
            modelBuilder.Entity<Product>()
                .HasOne(prod => prod.ProductType)
                .WithMany(productType => productType.Products)
                .HasForeignKey(prod => prod.ProductTypeId);
            
            modelBuilder.Entity<Increment>()
                .HasOne(inc => inc.Product)
                .WithMany(prod => prod.Increments)
                .HasForeignKey(inc => inc.ProductId);
            
            modelBuilder.Entity<Decrement>()
                .HasOne(decr => decr.Product)
                .WithMany(prod => prod.Decrements)
                .HasForeignKey(decr => decr.ProductId);
        }
    }
}