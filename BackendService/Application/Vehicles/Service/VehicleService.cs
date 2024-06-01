using BackendService.Application.Vehicles.Dtos;
using BackendService.Domain.Entities;
using BackendService.Helper.Api;
using BackendService.Helper.Model;
using BackendService.Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Application.Vehicles.Service;

public class VehicleService(ApplicationContext context) : IVehicleService
{
    private IQueryable<Vehicle> VehicleQuery()
    {
        return context.Vehicles
            .Where(e => e.IsDeleted == false)
            .AsQueryable();
    }
    public async Task<PaginatedList<VehicleReadDto>> GetVehicleAsync(PaginationFilter filter, CancellationToken cancellationToken)
    {
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

        var query = VehicleQuery();

        var totalRecords = await query.CountAsync(cancellationToken);

        var vehicles = await query
            .ProjectToType<VehicleReadDto>()
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync(cancellationToken);

        var hasNextPage = totalRecords > validFilter.PageSize * validFilter.PageNumber;
        var hasPreviousPage = validFilter.PageNumber > 1;

        if (vehicles.Count == 0)
        {
            return new PaginatedList<VehicleReadDto>(validFilter.PageNumber, validFilter.PageSize, totalRecords, new List<VehicleReadDto>(), hasPreviousPage, hasNextPage);
        }

        var result = new PaginatedList<VehicleReadDto>(validFilter.PageNumber, validFilter.PageSize, totalRecords, vehicles, hasPreviousPage, hasNextPage);

        return result;
    }

    public Task<VehicleReadDto> GetVehicleByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = VehicleQuery()
            .ProjectToType<VehicleReadDto>()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        return result!;
    }

    public async Task<VehicleReadDto> CreateVehicleAsync(VehicleWriteDto vehicleWriteDto, CancellationToken cancellationToken)
    {
        var vehicle = vehicleWriteDto.Adapt<Vehicle>();

        await context.Vehicles.AddAsync(vehicle, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return vehicle.Adapt<VehicleReadDto>();
    }

    public async Task<VehicleReadDto> UpdateVehicleAsync(Guid id, VehicleWriteDto vehicleWriteDto, CancellationToken cancellationToken)
    {
        var existingVehicle = await context.Vehicles.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (existingVehicle is null)
        {
            return null!;
        }

        existingVehicle = vehicleWriteDto.Adapt(existingVehicle);

        context.Vehicles.Update(existingVehicle);
        await context.SaveChangesAsync(cancellationToken);

        return existingVehicle.Adapt<VehicleReadDto>();
    }

    public async Task<VehicleReadDto> DeleteVehicleAsync(Guid id, CancellationToken cancellationToken)
    {
        var existingVehicle = await context.Vehicles.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (existingVehicle is null)
        {
            return null!;
        }

        existingVehicle.IsDeleted = true;
        existingVehicle.IsActive = false;

        context.Vehicles.Update(existingVehicle);
        await context.SaveChangesAsync(cancellationToken);

        return existingVehicle.Adapt<VehicleReadDto>();
    }
}
