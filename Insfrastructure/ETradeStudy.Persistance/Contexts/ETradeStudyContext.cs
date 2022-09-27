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
    public class ETradeStudyContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public ETradeStudyContext(DbContextOptions options) : base(options)
        {
        }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(User ID=root;Password=myPassword;Host=localhost;Port=5432;Database=myDataBase;);
        }*/
        public DbSet<Product> Products{ get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Domain.Entities.File> Files{ get; set; }
        public DbSet<ProductImageFile> ProductImages{ get; set; }
        public DbSet<InvoiceFile>InvoiceFiles { get; set; }
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
