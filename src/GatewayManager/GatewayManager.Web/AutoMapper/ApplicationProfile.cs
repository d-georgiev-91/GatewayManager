using AutoMapper;
using GatewayManager.Web.Models;

namespace GatewayManager.Web.AutoMapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<GatewayCreateModel, DataModels.Gateway>();
            CreateMap<PeripheralDeviceCreateModel, DataModels.PeripheralDevice>();
            CreateMap<DataModels.PeripheralDevice, PeripheralDevice>();
            CreateMap<Page, Services.Models.Page>();
            CreateMap<DataModels.Gateway, Gateway>();
            CreateMap(typeof(Services.Models.Paginated<>), typeof(Paginated<>));
            CreateMap<DataModels.Gateway, GatewayDetails>()
                .ForMember(dest => dest.PeripheralDevices,
                    opt => opt.MapFrom(g => g.PeripheralDevices));
        }
    }
}
