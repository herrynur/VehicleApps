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
    /// <summary>
    /// Api to get All Bookings
    /// </summary>
    /// <remarks>
    /// Use PaginationFilter to get paginated data with Parameters PageNumber and PageSize,
    /// example:
    /// 
    ///     {BaseUrl}Bookings?PageNumber=1&amp;PageSize=10
    ///     
    /// Use Query to search data with Parameters query,
    /// example:
    /// 
    ///     {BaseUrl}Bookings?PageNumber=1&amp;PageSize=10&amp;Query=mandiri
    ///     
    /// </remarks>
    /// <param name="filter"></param>
    /// <param name="status"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<PaginatedList<BookingReadDto>>> GetBookingsAsync(
        [FromQuery] PaginationFilter filter,
        [FromQuery] BookingStatus? status,
        CancellationToken cancellationToken)
    {
        return await bookingService.GetBookingsAsync(filter, status, cancellationToken);
    }

    /// <summary>
    /// Get Single Booking
    /// </summary>
    /// <remarks>
    /// Use id from Bookings data in path to get single data using GET Method, example:
    /// 
    ///     {BaseUrl}Bookings/cdf76df8-d15c-4a0b-9c77-ec50dead8dac
    ///     
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Post Booking
    /// </summary>
    /// <param name="bookingWriteDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<BookingReadDto>> CreateBookingAsync(
        [FromBody] BookingWriteDto bookingWriteDto,
        CancellationToken cancellationToken)
    {
        var result = await bookingService.CreateBookingAsync(bookingWriteDto, cancellationToken);

        return result;
    }
}
