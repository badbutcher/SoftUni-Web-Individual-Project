namespace StarCraft.Web.Models.Units
{
    using System.Collections.Generic;
    using StarCraft.Services.Models;

    public class UnitBuyViewModel
    {
        public IEnumerable<BasicUnitInfoServiceModel> Units { get; set; }
    }
}