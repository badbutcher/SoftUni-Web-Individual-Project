namespace StarCraft.Web.Models.Buildings
{
    using System.Collections.Generic;
    using StarCraft.Services.Models;
    using StarCraft.Web.Models.UsersViewModels;

    public class UserBuyBuildingsViewModel : UsersResources
    {
        public IEnumerable<BasicBuildingInfoServiceModel> Buildings { get; set; }
    }
}