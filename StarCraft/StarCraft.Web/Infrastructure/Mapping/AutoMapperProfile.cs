namespace StarCraft.Web.Infrastructure.Mapping
{
    using AutoMapper;
    using StarCraft.Data.Models;
    using StarCraft.Services.Models;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<Building, BasicBuildingInfoServiceModel>();
        }
    }
}