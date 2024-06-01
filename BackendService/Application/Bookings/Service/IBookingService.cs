using BackendService.Application.Bookings.Dtos;
using BackendService.Domain.Enums;
using BackendService.Helper.Api;
using BackendService.Helper.Model;

namespace BackendService.Application.Bookings.Service;

public interface IBookingService
{
    Task<PaginatedList<BookingReadDto>> GetBookingsAsync(PaginationFilter filter, BookingStatus? status, CancellationToken cancellationToken);
    Task<BookingReadDto> GetBookingByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<BookingReadDto> CreateBookingAsync(BookingWriteDto bookingWriteDto, CancellationToken cancellationToken);
}
