using BackendService.Domain.Enums;
using BackendService.Helper.Helper;

namespace BackendService.Domain.Entities;

public class Approval : EntityBase
{
    public ApprovalStatus? Status { get; set; }
    public MsUser? MsUser { get; set; }
    public Guid? MsUserId { get; set; }
    public Booking? Booking { get; set; }
    public Guid? BookingId { get; set; }
    public DateTime ApprovedAt { get; set; }
}
