using BackendService.Application.Vehicles.Dtos;

namespace BackendService.Application.VehicleUsages.Dtos;

public class VehicleUsageReadDto
{
    public Guid Id { get; set; }
    public Guid? VehicleId { get; set; }
    public DateOnly UsageDate { get; set; }
    public double FuelConsumption { get; set; }
    public string? ServiceDetails { get; set; }
    public VehicleReadDto? Vehicle { get; set; }
}
