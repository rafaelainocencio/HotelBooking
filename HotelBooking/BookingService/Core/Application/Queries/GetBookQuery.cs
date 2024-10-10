using Application.Booking.Responses;
using MediatR;

namespace Application.Queries
{
    public class GetBookingQuery : IRequest<BookingResponse>
    {
        public int Id { get; set; }
    }
}
