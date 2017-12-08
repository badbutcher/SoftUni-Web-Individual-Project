namespace StarCraft.Web.Areas.Admin.Models.Units
{
    using StarCraft.Services.Models;

    public class DeleteUnitModel
    {
        public UnitServiceModel Unit { get; set; }

        public int Id { get; set; }
    }
}