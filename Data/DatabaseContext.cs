using FleaMarket.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FleaMarket.Data
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

            builder.Entity<Item>()
                   .HasMany(i => i.Images)
                   .WithOne(i => i.Item)
                   .HasForeignKey(i => i.ItemId);
            builder.Entity<Item>()
                   .HasOne(i => i.Cover)
                   .WithOne()
                   .HasForeignKey<Item>(i => i.CoverId);
        }
    }
}
