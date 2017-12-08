namespace StarCraft.Web.Areas.Admin.Models.Buildings
{
    using StarCraft.Services.Models;

    public class DeleteBuildingModel
    {
        public BuildingServiceModel Building { get; set; }

        public int Id { get; set; }
    }
}