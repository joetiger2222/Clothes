using Clothes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clothes.Data
{
    public class DataDbContext : IdentityDbContext 
    {
        public DataDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
                
        }

        public DbSet<ClotheItem> Clothes { get; set; }
        public DbSet<Category> Categories { get; set;}
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Image> Images { get; set; }





        protected override void OnModelCreating(ModelBuilder builder)
        {
            var readerGuid = "376be536-4dd7-4a46-9b3a-062548c8acd8";
            var writerGuid = "552b36f8-3fb4-461d-8a62-a2f430e29330";
            base.OnModelCreating(builder);
            var roles = new List<IdentityRole>{
                new IdentityRole
                {
                    Id=readerGuid,
                    ConcurrencyStamp=readerGuid,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id=writerGuid,
                    ConcurrencyStamp=writerGuid,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
