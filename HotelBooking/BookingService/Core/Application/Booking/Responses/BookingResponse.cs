using Application.Booking.DTOs;

namespace Application.Booking.Responses
{
    public class BookingResponse : Response
    {
        public BookingDto Data { get; set; }
    }
}
