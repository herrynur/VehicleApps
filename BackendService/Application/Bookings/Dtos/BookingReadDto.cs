using BackendService.Application.Approvals.Dtos;
using BackendService.Application.Vehicles.Dtos;
using BackendService.Domain.Entities;
using Mapster;

namespace BackendService.Application.Bookings.Dtos;

public class BookingReadDto
{
    public Guid Id { get; set; }
    public DateOnly BookingDate { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public Guid? VehicleId { get; set; }
    public string? Status { get; set; }
    public VehicleReadDto? Vehicle { get; set; }
    public List<ApprovalReadDto> Approvals { get; set; } = [];
}

public class BookingReadDtoMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Booking, BookingReadDto>()
            .MaxDepth(2);
    }
}
