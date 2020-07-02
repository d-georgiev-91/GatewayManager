using AutoMapper;
using GatewayManager.DataModels;
using GatewayManager.Services;
using GatewayManager.Web.Models;

namespace GatewayManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase
    {
        private readonly IGatewayService _gatewayService;
        private readonly IMapper _mapper;

        public GatewayController(IGatewayService gatewayService, IMapper mapper)
        {
            _gatewayService = gatewayService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create(GatewayCreateModel gatewayCreateModel)
        {
            var gateway = _mapper.Map<GatewayCreateModel, Gateway>(gatewayCreateModel);
            _gatewayService.Add(gateway);

            return Ok();
        }
    }
}
