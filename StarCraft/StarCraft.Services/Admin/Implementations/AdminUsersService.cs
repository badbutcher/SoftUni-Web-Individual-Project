namespace StarCraft.Services.Admin.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using StarCraft.Data.Models;
    using StarCraft.Services.Admin.Contracts;
    using StarCraft.Services.Admin.Models;
    using StarCraft.Web.Data;
    using static StarCraft.Data.DataConstants;
    using static ServiceConstants;

    public class AdminUsersService : IAdminUsersService
    {
        private readonly StarCraftDbContext db;

        public AdminUsersService(StarCraftDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync(int page = FirstPage)
        {
            var result = await this.db.Users
                   .OrderByDescending(a => a.Id)
                   .Skip((page - FirstPage) * PageSize)
                   .Take(PageSize)
                   .ProjectTo<AdminUserListingServiceModel>()
                   .ToListAsync();

            return result;
        }

        public async Task AddResourcesAsync(string userId, int minerals, int gas)
        {
            User user = await this.db.Users.FirstOrDefaultAsync(a => a.Id == userId);

            if (user.Minerals + minerals > MaxUserMineralCapacity)
            {
                user.Minerals = MaxUserMineralCapacity;
            }
            else
            {
                user.Minerals += minerals;
            }

            if (user.Gas + gas > MaxUserGasCapacity)
            {
                user.Gas = MaxUserGasCapacity;
            }
            else
            {
                user.Gas += gas;
            }

            await this.db.SaveChangesAsync();
        }

        public async Task<int> TotalAsync()
        {
            int result = await this.db.Users.CountAsync();

            return result;
        }
    }
}