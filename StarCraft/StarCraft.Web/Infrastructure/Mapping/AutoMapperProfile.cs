namespace StarCraft.Web.Infrastructure.Mapping
{
    using System.Linq;
    using AutoMapper;
    using StarCraft.Data.Models;
    using StarCraft.Services.Models;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<Building, BasicBuildingInfoServiceModel>();

            this.CreateMap<User, UserInfoBattleServiceModel>()
                .ForMember(a => a.ArmyQuantity, opt => opt.MapFrom(c => c.Units.Sum(d => d.Quantity)));
        }
    }
}