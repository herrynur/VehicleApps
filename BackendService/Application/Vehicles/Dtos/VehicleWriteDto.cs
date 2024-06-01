using BackendService.Domain.Enums;

namespace BackendService.Application.Vehicles.Dtos;

public class VehicleWriteDto
{
    public string? VehicleNumber { get; set; }
    public VehicleStatus Status { get; set; }
    public VehicleType Type { get; set; }
}
