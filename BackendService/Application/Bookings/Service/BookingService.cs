using BackendService.Application.Bookings.Dtos;
using BackendService.Application.Common.Identity;
using BackendService.Domain.Entities;
using BackendService.Domain.Enums;
using BackendService.Helper.Api;
using BackendService.Helper.Model;
using BackendService.Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Application.Bookings.Service;

public class BookingService(ApplicationContext context, IIdentityService identityService) : IBookingService
{
    private IQueryable<Booking> BookingQuery()
    {
        return context.Bookings
            .Where(e => e.IsDeleted == false)
            .AsQueryable();
    }
    public async Task<BookingReadDto> CreateBookingAsync(BookingWriteDto bookingWriteDto, CancellationToken cancellationToken)
    {
        var booking = bookingWriteDto.Adapt<Booking>();
        booking.MsUserId = Guid.Parse(identityService.GetUserId());
        booking.Status = BookingStatus.Pending;

        await context.Bookings.AddAsync(booking, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return booking.Adapt<BookingReadDto>();
    }

    public Task<BookingReadDto> GetBookingByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var booking = context.Bookings
            .Where(e => e.Id == id)
            .ProjectToType<BookingReadDto>()
            .FirstOrDefaultAsync(cancellationToken);

        return booking!;
    }

    public async Task<PaginatedList<BookingReadDto>> GetBookingsAsync(PaginationFilter filter, BookingStatus? status, CancellationToken cancellationToken)
    {
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

        var query = BookingQuery();

        if (status.HasValue)
        {
            query = query.Where(e => e.Status == status);
        }

        var totalRecords = await query.CountAsync(cancellationToken);

        var bookings = await query
            .ProjectToType<BookingReadDto>()
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync(cancellationToken);

        var hasNextPage = totalRecords > validFilter.PageSize * validFilter.PageNumber;
        var hasPreviousPage = validFilter.PageNumber > 1;

        if (bookings.Count == 0)
        {
            return new PaginatedList<BookingReadDto>(validFilter.PageNumber, validFilter.PageSize, totalRecords, new List<BookingReadDto>(), hasPreviousPage, hasNextPage);
        }

        var result = new PaginatedList<BookingReadDto>(validFilter.PageNumber, validFilter.PageSize, totalRecords, bookings, hasPreviousPage, hasNextPage);

        return result;
    }
}
