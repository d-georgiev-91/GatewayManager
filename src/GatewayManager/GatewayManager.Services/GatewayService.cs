﻿using System.Threading.Tasks;
using GatewayManager.Data;
using GatewayManager.DataModels;
using Microsoft.EntityFrameworkCore;

namespace GatewayManager.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly IDataService<Gateway> _gatewayDataService;

        public GatewayService(IDataService<Gateway> gatewayDataService) => _gatewayDataService = gatewayDataService;

        public async Task<ServiceResult> AddAsync(Gateway gateway)
        {
            var result = new ServiceResult();
            var existingGateway = await _gatewayDataService.GetByIdAsync(gateway.SerialNumber);

            if (existingGateway != null)
            {
                return result.AddError(ErrorType.InvalidInput, "Gateway with such serial number already exists");
            }

            await _gatewayDataService.AddAsync(gateway);
            await _gatewayDataService.SaveChangesAsync();

            return result;
        }

        public async Task<ServiceResult<Gateway>> FindAsync(string serialNumber)
        {
            var result = new ServiceResult<Gateway>();

            var gateway = await _gatewayDataService.Filter(g => g.SerialNumber == serialNumber, 
                    g => g.PeripheralDevices)
                .SingleOrDefaultAsync();

            if (gateway != null)
            {
                result.Data = gateway;
            }
            else
            {
                result.AddError(ErrorType.NotFound, $"No such gateway with serial number: {serialNumber}");
            }

            return result;
        }
    }
}
