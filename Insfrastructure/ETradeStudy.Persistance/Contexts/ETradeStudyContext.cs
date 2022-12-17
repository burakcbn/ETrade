using ETradeStudy.Domain.Entities;
using ETradeStudy.Domain.Entities.Common;
using ETradeStudy.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Contexts
{
    public class ETradeStudyContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ETradeStudyContext(DbContextOptions options) : base(options)
        {
        }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(User ID=root;Password=myPassword;Host=localhost;Port=5432;Database=myDataBase;);
        }*/

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<CompletedOrder> CompletedOrders { get; set; }
        public DbSet<Menu>Menus{ get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
             .HasKey(b => b.Id);

            builder.Entity<Order>()
                .HasIndex(o => o.OrderCode)
                .IsUnique();


            builder.Entity<Order>()
                           .HasOne(o => o.CompletedOrder)
                           .WithOne(c => c.Order)
                           .HasForeignKey<CompletedOrder>(c => c.OrderId);

            builder.Entity<Basket>()
                .HasOne(b => b.Order)
                .WithOne(o => o.Basket)
                .HasForeignKey<Order>(b => b.Id);

            base.OnModelCreating(builder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var results = ChangeTracker.Entries<BaseEntity>();

            foreach (var result in results)
            {
                _ = result.State switch
                {
                    EntityState.Added => result.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => result.Entity.UpdateDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
