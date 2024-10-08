using Application.Booking.DTOs;
using Application.Booking.Ports;
using Application.Booking.Requests;
using Application.Booking.Responses;
using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booking
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepositoy _bookingRepository;
        public BookingManager(IBookingRepositoy bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<BookingResponse> CreateBooking(CreateBookingRequest request)
        {
            try
            {
                var booking = BookingDto.MapToEntity(request.Data);
                booking.SaveAsync(_bookingRepository);

                request.Data.Id = booking.Id;

                return new BookingResponse
                { 
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (InvalidPlacedAtException e)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.INVALID_PLACED_AT,
                    Message = "The place is not valid."
                };
            }
            catch (InvalidStartException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.INVALID_START,
                    Message = "The start date is not valid."
                };
            }
            catch (InvalidEndException e)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.INVALID_END,
                    Message = "The end date is not valid."
                };
            }
            catch (InvalidRoomException e)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.INVALID_ROOM,
                    Message = "The room is not valid."
                };
            }
            catch (InvalidGuestException e)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.INVALID_GUEST,
                    Message = "The guest is not valid."
                };
            }      
            catch (Exception ex)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.UNKNOWN_ERROR,
                    Message = "There was an error when saving to DB."
                };
            }
        }

        public Task<BookingResponse> GetBooking(int guestId)
        {
            throw new NotImplementedException();
        }
    }
}
