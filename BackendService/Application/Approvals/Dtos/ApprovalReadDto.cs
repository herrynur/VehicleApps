using BackendService.Application.Bookings.Dtos;
using BackendService.Domain.Enums;

namespace BackendService.Application.Approvals.Dtos;

public class ApprovalReadDto
{
    public Guid Id { get; set; }
    public ApprovalStatus? Status { get; set; }
    public Guid? BookingId { get; set; }
    public BookingReadDto? Booking { get; set; }
}
