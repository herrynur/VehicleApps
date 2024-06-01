using BackendService.Application.VehicleUsages.Dtos;

namespace BackendService.Application.VehicleUsages.Service;

public interface IVehicleUsageService
{
    Task<VehicleUsageReadDto> PostVehicleUsageAsync(VehicleUsageWriteDto vehicleUsageWriteDto, CancellationToken cancellationToken);
}
