namespace StarCraft.Web.Models.UsersViewModels
{
    using System.Collections.Generic;
    using StarCraft.Services.Models;

    public class UserBuyUnitsViewModel : UsersResources
    {
        public IEnumerable<BasicUnitInfoServiceModel> Units { get; set; }

        public IEnumerable<UnitBasicStatsServiceModel> BoughtUnits { get; set; }
    }
}