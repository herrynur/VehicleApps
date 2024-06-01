using BackendService.Application.Bookings.Dtos;
using BackendService.Application.Bookings.Service;
using BackendService.Domain.Enums;
using BackendService.Helper.Api;
using BackendService.Helper.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BookingsController(IBookingService bookingService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<BookingReadDto>>> GetBookingsAsync(
        [FromQuery] PaginationFilter filter,
        [FromQuery] BookingStatus? status,
        CancellationToken cancellationToken)
    {
        return await bookingService.GetBookingsAsync(filter, status, cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingReadDto>> GetBookingByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await bookingService.GetBookingByIdAsync(id, cancellationToken);

        if (result is null)
        {
            return Problem(statusCode: 404, detail: "Booking not found");
        }

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<BookingReadDto>> CreateBookingAsync(
        [FromBody] BookingWriteDto bookingWriteDto,
        CancellationToken cancellationToken)
    {
        var result = await bookingService.CreateBookingAsync(bookingWriteDto, cancellationToken);

        return result;
    }
}
