namespace BackendService.Application.VehicleUsages.Dtos;

public class VehicleUsageWriteDto
{
    public Guid? VehicleId { get; set; }
    public DateOnly UsageDate { get; set; }
    public double FuelConsumption { get; set; }
    public string? ServiceDetails { get; set; }
}
