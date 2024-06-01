using BackendService.Domain.Enums;

namespace BackendService.Application.Approvals.Dtos;

public class ApprovalWriteDto
{
    public Guid? BookingId { get; set; }
}
