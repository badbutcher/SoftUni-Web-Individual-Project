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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}