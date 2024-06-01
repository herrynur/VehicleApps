namespace BackendService.Application.Bookings.Dtos;

public class BookingWriteDto
{
    public DateOnly BookingDate { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public Guid? VehicleId { get; set; }
}
