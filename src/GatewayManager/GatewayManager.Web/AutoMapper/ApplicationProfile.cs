using AutoMapper;
using GatewayManager.DataModels;

namespace GatewayManager.Web.AutoMapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Models.GatewayCreateModel, Gateway>();
        }
    }
}
