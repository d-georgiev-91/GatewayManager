using AutoMapper;
using GatewayManager.Web.Models;

namespace GatewayManager.Web.Tests.AutoMapper
{
    public class TestsProfile : Profile
    {
        public TestsProfile()
        {
            CreateMap<GatewayCreateModel, DataModels.Gateway>();
            CreateMap<DataModels.PeripheralDevice, PeripheralDevice>();
            CreateMap<DataModels.Gateway, GatewayDetails>()
                .ForMember(dest => dest.PeripheralDevices,
                    opt => opt.MapFrom(g => g.PeripheralDevices));
        }
    }
}
