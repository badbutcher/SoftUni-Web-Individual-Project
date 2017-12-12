namespace StarCraft.Services.Admin.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using StarCraft.Services.Admin.Contracts;
    using StarCraft.Services.Admin.Models;
    using StarCraft.Web.Data;

    public class AdminUsersService : IAdminUsersService
    {
        private readonly StarCraftDbContext db;

        public AdminUsersService(StarCraftDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync(int page = 1)
        {
            var result = await this.db.Users
                   .OrderByDescending(a => a.Id)
                   .Skip((page - 1) * 25)
                   .Take(25)
                   .ProjectTo<AdminUserListingServiceModel>()
                   .ToListAsync();

            return result;
        }

        public async Task AddResources(string userId, int minerals, int gas)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(a => a.Id == userId);

            user.Minerals += minerals;
            user.Gas += gas;

            await this.db.SaveChangesAsync();
        }

        public async Task<int> TotalAsync()
        {
            var result = await this.db.Users.CountAsync();

            return result;
        }
    }
}