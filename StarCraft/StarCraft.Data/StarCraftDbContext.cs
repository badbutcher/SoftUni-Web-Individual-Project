namespace StarCraft.Web.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using StarCraft.Data.Models;

    public class StarCraftDbContext : IdentityDbContext<User>
    {
        public StarCraftDbContext(DbContextOptions<StarCraftDbContext> options)
            : base(options)
        {
        }

        public DbSet<Building> Buildings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserBuilding>()
               .HasKey(a => new { a.UserId, a.BuildingId });

            builder.Entity<User>()
                .HasMany(a => a.Buildings)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.Entity<Building>()
                .HasMany(a => a.Users)
                .WithOne(a => a.Building)
                .HasForeignKey(a => a.BuildingId);

            base.OnModelCreating(builder);
        }
    }
}