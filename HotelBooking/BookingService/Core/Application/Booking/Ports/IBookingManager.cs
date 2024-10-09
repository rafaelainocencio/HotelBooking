using Application.Booking.Requests;
using Application.Booking.Responses;
using Application.Payment.Dtos;
using Application.Payment.Responses;

namespace Application.Booking.Ports
{
    public interface IBookingManager
    {
        Task<BookingResponse> CreateBooking(CreateBookingRequest request);
        Task<BookingResponse> GetBooking(int guestId);
        Task<PaymentResponse> PayForABooking(PaymentRequestDto paymentRequestDto);
    }
}
