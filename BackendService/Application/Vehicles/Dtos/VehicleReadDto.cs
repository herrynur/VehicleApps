using BackendService.Application.VehicleUsages.Dtos;
using BackendService.Domain.Entities;
using Mapster;

namespace BackendService.Application.Vehicles.Dtos;

public class VehicleReadDto
{
    public Guid Id { get; set; }
    public string? VehicleNumber { get; set; }
    public string? Status { get; set; }
    public string? Type { get; set; }
    public ICollection<VehicleUsageReadDto> VehicleUsages { get; set; } = [];
}

public class VehicleReadDtoMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Vehicle, VehicleReadDto>()
            .MaxDepth(2);
    }
}   
