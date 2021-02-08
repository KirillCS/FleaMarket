using FleaMarket.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FleaMarket.Domain
{
    public class DatabaseContext : IdentityDbContext<User> 
    {
        public DbSet<Image> Images { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Item>()
                   .Property(i => i.TradeEnabled)
                   .HasDefaultValue(false);
            builder.Entity<Item>()
                   .Property(i => i.PublishingDate)
                   .HasDefaultValueSql("getutcdate()");

            builder.Entity<Image>()
                   .Property(i => i.IsCover)
                   .HasDefaultValue(false);
        }
    }
}
