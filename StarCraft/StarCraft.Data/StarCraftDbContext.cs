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

        public DbSet<Unit> Units { get; set; }

        public DbSet<UserBuilding> UserBuilding { get; set; }

        public DbSet<BuildingUnit> BuildingUnit { get; set; }

        public DbSet<UnitUser> UnitUser { get; set; }

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

            builder.Entity<BuildingUnit>()
               .HasKey(a => new { a.BuildingId, a.UnitId });

            builder.Entity<Building>()
                .HasMany(a => a.Units)
                .WithOne(a => a.Building)
                .HasForeignKey(a => a.BuildingId);

            builder.Entity<Unit>()
                .HasMany(a => a.Buildings)
                .WithOne(a => a.Unit)
                .HasForeignKey(a => a.UnitId);

            builder.Entity<UnitUser>()
              .HasKey(a => new { a.UnitId, a.UserId });

            builder.Entity<User>()
                .HasMany(a => a.Units)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.Entity<Unit>()
                .HasMany(a => a.Users)
                .WithOne(a => a.Unit)
                .HasForeignKey(a => a.UnitId);

            base.OnModelCreating(builder);
        }
    }
}