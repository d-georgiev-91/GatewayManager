using System.Threading.Tasks;
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
        public async Task<IActionResult> Create(GatewayCreateModel gatewayCreateModel)
        {
            var gateway = _mapper.Map<GatewayCreateModel, Gateway>(gatewayCreateModel);
            var result = await _gatewayService.AddAsync(gateway);

            if (result.HasErrors && result.Errors.ContainsKey(ErrorType.InvalidInput))
            {
                return BadRequest(result.Errors[ErrorType.InvalidInput].Message);
            }

            return CreatedAtAction(nameof(GetDetails), new { gatewayCreateModel.SerialNumber }, gatewayCreateModel);
        }

        [HttpGet]
        [Route("{serialNumber}/details")]
        public async Task<IActionResult> GetDetails(string serialNumber)
        {
            var result = await _gatewayService.FindAsync(serialNumber);

            if (result.Errors.ContainsKey(ErrorType.NotFound))
            {
                return NotFound(result.Errors[ErrorType.NotFound]);
            }

            var responseData = _mapper.Map<Gateway, GatewayDetails>(result.Data);

            return Ok(responseData);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll(Page page)
        {
            var result = _gatewayService.GetAll(_mapper.Map<Page>(page));

            if (result.Errors.ContainsKey(ErrorType.UnexpectedError))
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<Models.Paginated<Models.WebSiteDetailed>>(result.Data));
        }
    }
}