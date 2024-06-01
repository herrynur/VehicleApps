using BackendService.Application.Vehicles.Dtos;
using BackendService.Helper.Api;
using BackendService.Helper.Model;

namespace BackendService.Application.Vehicles.Service;

public interface IVehicleService
{
    Task<PaginatedList<VehicleReadDto>> GetVehicleAsync(PaginationFilter filter, CancellationToken cancellationToken);
    Task<VehicleReadDto> GetVehicleByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<VehicleReadDto> CreateVehicleAsync(VehicleWriteDto vehicleWriteDto, CancellationToken cancellationToken);
    Task<VehicleReadDto> UpdateVehicleAsync(Guid id, VehicleWriteDto vehicleWriteDto, CancellationToken cancellationToken);
    Task<VehicleReadDto> DeleteVehicleAsync(Guid id, CancellationToken cancellationToken);
}
