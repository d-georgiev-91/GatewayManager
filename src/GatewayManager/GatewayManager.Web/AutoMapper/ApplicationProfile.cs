using AutoMapper;
using GatewayManager.Web.Models;

namespace GatewayManager.Web.AutoMapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<GatewayCreateModel, DataModels.Gateway>();
            CreateMap<DataModels.PeripheralDevice, PeripheralDevice>();
            CreateMap<DataModels.Gateway, GatewayDetails>()
                .ForMember(dest => dest.PeripheralDevices,
                    opt => opt.MapFrom(g => g.PeripheralDevices));
        }
    }
}
