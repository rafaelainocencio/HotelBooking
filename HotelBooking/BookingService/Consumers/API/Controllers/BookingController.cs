﻿using Application;
using Application.Booking.DTOs;
using Application.Booking.Ports;
using Application.Booking.Requests;
using Application.Payment.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("controller")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingManager _bookingManager;
        private readonly ILogger<GuestController> _logger;

        public BookingController(IBookingManager bookingManager,
                                 ILogger<GuestController> logger)
        {
            _bookingManager = bookingManager;
            _logger = logger;
        }

        [HttpPost]
        [Route("bookingId/Pay")]
        public async Task<ActionResult<BookingDto>> Pay(PaymentRequestDto paymentRequestDto, int bookingId)
        {
            paymentRequestDto.BookingId = bookingId;
            var res = await _bookingManager.PayForABooking(paymentRequestDto);
            
            if (res.Success) return Ok(res.Data);

            return BadRequest(res);
        }   

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post(BookingDto booking)
        {
            var request = new CreateBookingRequest
            {
                Data = booking
            };

            var res = await _bookingManager.CreateBooking(request);

            if (res.Success) return Created("", res);

            if (res.ErrorCode == ErrorCodes.INVALID_PLACED_AT)
            {
                return BadRequest(res);
            }
            if (res.ErrorCode == ErrorCodes.INVALID_START)
            {
                return BadRequest(res);
            }
            if (res.ErrorCode == ErrorCodes.INVALID_END)
            {
                return BadRequest(res);
            }
            if (res.ErrorCode == ErrorCodes.INVALID_ROOM)
            {
                return BadRequest(res);
            }
            if (res.ErrorCode == ErrorCodes.INVALID_GUEST)
            {
                return BadRequest(res);
            }
            if (res.ErrorCode == ErrorCodes.MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unknown Returned", res);
            return BadRequest(500);
        }
    }
}
