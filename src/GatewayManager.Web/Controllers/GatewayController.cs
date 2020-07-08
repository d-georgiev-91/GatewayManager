using System.Threading.Tasks;
using AutoMapper;
using GatewayManager.Services;
using GatewayManager.Web.Models;

namespace GatewayManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("[controller]")]
    public class GatewayController : BaseController
    {
        private readonly IGatewayService _gatewayService;
        private readonly IGatewayDevicesManagerService _gatewayDevicesManagerService;

        public GatewayController(IGatewayService gatewayService,
            IGatewayDevicesManagerService gatewayDevicesManagerService, IMapper mapper)
            : base(mapper)
        {
            _gatewayService = gatewayService;
            _gatewayDevicesManagerService = gatewayDevicesManagerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(GatewayCreateModel gatewayCreateModel)
        {
            var gateway = Mapper.Map<GatewayCreateModel, DataModels.Gateway>(gatewayCreateModel);
            var result = await _gatewayService.AddAsync(gateway);

            if (result.HasErrors && result.Errors.ContainsKey(ErrorType.InvalidInput))
            {
                return BadRequest(result.Errors[ErrorType.InvalidInput].Message);
            }

            return CreatedAtAction(nameof(GetDetails), new { gatewayCreateModel.SerialNumber }, gatewayCreateModel);
        }

        [HttpGet]
        [Route("{serialNumber}")]
        public async Task<IActionResult> GetDetails(string serialNumber)
        {
            var result = await _gatewayService.FindAsync(serialNumber);

            if (result.Errors.ContainsKey(ErrorType.NotFound))
            {
                return NotFound(result.Errors[ErrorType.NotFound].Message);
            }

            var responseData = Mapper.Map<DataModels.Gateway, GatewayDetails>(result.Data);

            return Ok(responseData);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync([FromQuery] Page page)
        {
            var serviceResult = await _gatewayService.GetAllAsync(Mapper.Map<Page, Services.Models.Page>(page));
            var pageResult = Mapper.Map<Services.Models.Paginated<DataModels.Gateway>, Paginated<Gateway>>(serviceResult.Data);

            return Ok(pageResult);
        }

        [HttpPost]
        [Route("{serialNumber}/AddDevice/{peripheralDeviceId}")]
        public async Task<IActionResult> AssignPeripheralDevice(string serialNumber, long peripheralDeviceId)
        {
            var serviceResult = await _gatewayDevicesManagerService.AssignPeripheralDeviceAsync(serialNumber, peripheralDeviceId);

            return ConvertDevicesManagerServiceResultToActionResult(serviceResult);
        }

        [HttpPost]
        [Route("{serialNumber}/RemoveDevice/{peripheralDeviceId}")]
        public async Task<IActionResult> RemovePeripheralDevice(string serialNumber, long peripheralDeviceId)
        {
            var serviceResult = await _gatewayDevicesManagerService.RemovePeripheralDeviceAsync(serialNumber, peripheralDeviceId);

            return ConvertDevicesManagerServiceResultToActionResult(serviceResult);
        }

        private IActionResult ConvertDevicesManagerServiceResultToActionResult(ServiceResult serviceResult)
        {
            if (serviceResult.Errors.ContainsKey(ErrorType.NotFound))
            {
                return NotFound(serviceResult.Errors[ErrorType.NotFound].Message);
            }

            if (serviceResult.Errors.ContainsKey(ErrorType.InvalidInput))
            {
                return BadRequest(serviceResult.Errors[ErrorType.InvalidInput].Message);
            }

            return Ok();
        }
    }
}