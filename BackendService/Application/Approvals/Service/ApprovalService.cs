using BackendService.Application.Approvals.Dtos;
using BackendService.Application.Common.Identity;
using BackendService.Domain.Entities;
using BackendService.Domain.Enums;
using BackendService.Helper.Exceptions;
using BackendService.Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Application.Approvals.Service;

public class ApprovalService(ApplicationContext context,
    IIdentityService identityService) : IApprovalService
{
    public async Task<ApprovalReadDto> ApproveBooking(ApprovalWriteDto approvalWriteDto, CancellationToken cancellationToken)
    {
        var booking = await context.Bookings.FirstOrDefaultAsync(x => x.Id == approvalWriteDto.BookingId, cancellationToken);

        if (booking == null)
        {
            throw new NotFoundException("Booking not found");
        }

        var approval = approvalWriteDto.Adapt<Approval>();
        approval.Status = ApprovalStatus.Approved;
        approval.ApprovedAt = DateTime.UtcNow;
        approval.MsUserId = Guid.Parse(identityService.GetUserId());

        await context.Approvals.AddAsync(approval, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        //Approve booking if approval count is greater than 2
        var approvalCount = await context.Approvals.CountAsync(x => x.BookingId == approvalWriteDto.BookingId, cancellationToken);

        if (approvalCount >= 2)
        {
            booking.Status = BookingStatus.Approved;
            booking.UpdatedAt = DateTime.UtcNow;

            context.Bookings.Update(booking);
            await context.SaveChangesAsync(cancellationToken);
        }

        return approval.Adapt<ApprovalReadDto>();
    }

    public async Task<ApprovalReadDto> RejectBooking(ApprovalWriteDto approvalWriteDto, CancellationToken cancellationToken)
    {
        var booking = context.Bookings.AnyAsync(x => x.Id == approvalWriteDto.BookingId, cancellationToken);

        if (booking == null)
        {
            throw new NotFoundException("Booking not found");
        }

        var approval = approvalWriteDto.Adapt<Approval>();
        approval.Status = ApprovalStatus.Rejected;
        approval.MsUserId = Guid.Parse(identityService.GetUserId());
        approval.ApprovedAt = DateTime.UtcNow;

        await context.Approvals.AddAsync(approval, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return approval.Adapt<ApprovalReadDto>();
    }
}
