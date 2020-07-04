using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace GatewayManager.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMapper Mapper;

        public BaseController(IMapper mapper) => Mapper = mapper;
    }
}