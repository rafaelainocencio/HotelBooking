using Application.Booking.Ports;
using Application.Booking.Responses;
using MediatR;

namespace Application.Booking.Commands.Handlers
{
    public class CreateBookCommandHandlers : IRequestHandler<CreateBookCommand, BookingResponse>
    {
        private readonly IBookingManager _bookingManager;

        public CreateBookCommandHandlers(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }

        public Task<BookingResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {

            return _bookingManager.CreateBooking(request.Booking);
        }
    }
}
