using System.Threading.Tasks;
using AutoMapper;
using GatewayManager.Services;
using GatewayManager.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GatewayManager.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeripheralDeviceController : BaseController
    {
        private readonly IPeripheralDeviceService _peripheralDeviceService;

        public PeripheralDeviceController(IPeripheralDeviceService peripheralDeviceService, IMapper mapper) : 
            base(mapper) => _peripheralDeviceService = peripheralDeviceService;

        [HttpPost]
        public async Task<IActionResult> Create(PeripheralDeviceCreateModel peripheralDeviceCreateModel)
        {
            var peripheralDevice = Mapper.Map<PeripheralDeviceCreateModel, DataModels.PeripheralDevice>(peripheralDeviceCreateModel);
            await _peripheralDeviceService.AddAsync(peripheralDevice);

            return Ok();
        }
    }
}
