using BackendService.Application.Approvals.Dtos;

namespace BackendService.Application.Approvals.Service;

public interface IApprovalService
{
    Task<ApprovalReadDto> ApproveBooking(ApprovalWriteDto approvalWriteDto, CancellationToken cancellationToken);
    Task<ApprovalReadDto> RejectBooking(ApprovalWriteDto approvalWriteDto, CancellationToken cancellationToken);
}
