using BackendService.Application.Approvals.Dtos;
using BackendService.Application.Approvals.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ApprovalsController(IApprovalService approvalService) : ControllerBase
{
    /// <summary>
    /// Api to Approve Booking
    /// </summary>
    /// <param name="approvalWriteDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("Approve")]
    public async Task<ActionResult<ApprovalReadDto>> ApproveBookingAsync(
        [FromBody] ApprovalWriteDto approvalWriteDto,
        CancellationToken cancellationToken)
    {
        var result = await approvalService.ApproveBooking(approvalWriteDto, cancellationToken);

        return result;
    }

    /// <summary>
    /// Api to Reject Booking
    /// </summary>
    /// <param name="approvalWriteDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("Reject")]
    public async Task<ActionResult<ApprovalReadDto>> RejectBookingAsync(
        [FromBody] ApprovalWriteDto approvalWriteDto,
        CancellationToken cancellationToken)
    {
        var result = await approvalService.RejectBooking(approvalWriteDto, cancellationToken);

        return result;
    }
}
