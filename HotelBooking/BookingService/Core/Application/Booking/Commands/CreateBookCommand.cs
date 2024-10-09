using Application.Booking.DTOs;
using Application.Booking.Requests;
using Application.Booking.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booking.Commands
{
    public class CreateBookCommand : IRequest<BookingResponse>
    {
        public CreateBookingRequest Booking { get; set; }
    }
}
