using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.API.Entities;

namespace Store.API.DataContext
{
    public class ApplicationContext : IdentityDbContext<AppUser,AppRole, int>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
        {

        }
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Base> Bases { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Base>()
                .HasOne(i => i.City)
                .WithMany(i => i.Bases)
                .HasForeignKey(i => i.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}