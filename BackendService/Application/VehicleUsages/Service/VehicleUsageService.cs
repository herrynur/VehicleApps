using BackendService.Application.VehicleUsages.Dtos;
using BackendService.Domain.Entities;
using BackendService.Helper.Exceptions;
using BackendService.Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Application.VehicleUsages.Service;

public class VehicleUsageService(ApplicationContext context) : IVehicleUsageService
{
    public async Task<VehicleUsageReadDto> PostVehicleUsageAsync(VehicleUsageWriteDto vehicleUsageWriteDto, CancellationToken cancellationToken)
    {
        var vehicleExist = await context.Vehicles.AnyAsync(e => e.Id == vehicleUsageWriteDto.VehicleId, cancellationToken);

        if (!vehicleExist)
        {
            throw new NotFoundException("Vehicle");
        }

        var vehicleUsage = vehicleUsageWriteDto.Adapt<VehicleUsage>();

        await context.VehicleUsages.AddAsync(vehicleUsage, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return vehicleUsage.Adapt<VehicleUsageReadDto>();
    }
}
