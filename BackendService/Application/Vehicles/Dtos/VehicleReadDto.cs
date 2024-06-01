namespace BackendService.Application.Vehicles.Dtos;

public class VehicleReadDto
{
    public Guid Id { get; set; }
    public string? VehicleNumber { get; set; }
    public string? Status { get; set; }
    public string? Type { get; set; }
}
