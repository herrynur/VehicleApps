using BackendService.Helper.Helper;

namespace BackendService.Domain.Entities;

public class VehicleUsage : EntityBase
{
    public Vehicle? Vehicle { get; set; }
    public Guid? VehicleId { get; set; }
    public DateOnly UsageDate { get; set; }
    public double FuelConsumption { get; set; }
    public string? ServiceDetails { get; set; }
}
