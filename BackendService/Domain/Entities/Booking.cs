using BackendService.Domain.Enums;
using BackendService.Helper.Helper;

namespace BackendService.Domain.Entities;

public class Booking : EntityBase
{
    public DateOnly BookingDate { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public BookingStatus? Status { get; set; }
    public MsUser? MsUser { get; set; }
    public Guid? MsUserId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public Guid? VehicleId { get; set; }
    public ICollection<Approval> Approvals { get; set; } = [];
}
