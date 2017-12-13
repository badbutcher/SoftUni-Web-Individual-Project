namespace StarCraft.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StarCraft.Services.Admin.Models;

    public interface IAdminUsersService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();

        Task AddResourcesAsync(string userId, int minerals, int gas);
    }
}