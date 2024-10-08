﻿using Application.Booking.DTOs;
using Application.Booking.Ports;
using Application.Booking.Requests;
using Application.Booking.Responses;
using Application.Payment.Dtos;
using Application.Payment.Ports;
using Application.Payment.Responses;
using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Guest.Exceptions;
using Domain.Guest.Ports;
using Domain.Room.Exceptions;
using Domain.Room.Ports;
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
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IPaymentProcessorFactory _paymentProcessorFactory;

        public BookingManager(IBookingRepositoy bookingRepository,
                              IGuestRepository guestRepository,
                              IRoomRepository roomRepository,
                              IPaymentProcessorFactory paymentProcessorFactory)
        {
            _bookingRepository = bookingRepository;
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
            _paymentProcessorFactory = paymentProcessorFactory;
        }
        public async Task<BookingResponse> CreateBooking(CreateBookingRequest request)
        {
            try
            {
                var booking = BookingDto.MapToEntity(request.Data);
                booking.Guest = await _guestRepository.Get(request.Data.GuestId);
                booking.Room = await _roomRepository.Get(request.Data.RoomId);

                await booking.SaveAsync(_bookingRepository);

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

        public async Task<PaymentResponse> PayForABooking(PaymentRequestDto paymentRequestDto)
        {
            var paymentProcessor = _paymentProcessorFactory.GetPaymentProcessor(paymentRequestDto.SelectedPaymentProvider);

            var response = await paymentProcessor.CapturePayment(paymentRequestDto.PaymentIntention);

            if (response.Success)
            {
                return new PaymentResponse
                {
                    Success = true,
                    Data = response.Data,
                    Message = "Payment was successful."
                };
            }

            return response;
        }
    }
}
