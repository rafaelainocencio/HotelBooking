﻿using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Guest.Enums;
using Domain.Guest.Exceptions;
using Domain.Room.Exceptions;
using Action = Domain.Guest.Enums.Action;
using DomainEntities = Domain;

namespace Domain.Booking.Entities
{
    public class Booking
    {
        public Booking()
        {
            Status = Status.Created;
        }

        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Status Status { get; set; }
        public Status CurrentStatus { get { return Status; } }
        public DomainEntities.Room.Entities.Room Room { get; set; }
        public DomainEntities.Guest.Entities.Guest Guest { get; set; }

        public void ChangeState(Action action)
        {
            Status = (Status, action) switch
            {
                (Status.Created, Action.Pay) => Status.Paid,
                (Status.Created, Action.Cancel) => Status.Canceled,
                (Status.Paid, Action.Finish) => Status.Finished,
                (Status.Paid, Action.Refound) => Status.Refounded,
                (Status.Canceled, Action.Reopen) => Status.Created,
                _ => Status
            };
        }

        public async Task SaveAsync(IBookingRepositoy bookingRepository)
        {
            ValidadeState();

            if (Id == 0)
            {
                var resp = await bookingRepository.Create(this);
                this.Id = resp;
            }
            else
            {

            }
        }
        private void ValidadeState()
        {
            if (PlacedAt == default)
            {
                throw new InvalidPlacedAtException();
            }
            if (Start == default)
            {
                throw new InvalidStartException();
            }
            if (End == default)
            {
                throw new InvalidEndException();
            }
            if (!Room.IsValid())
            {
                throw new InvalidRoomException();
            }
            if (!Guest.IsValid())
            {
                throw new InvalidGuestException();
            }

            if (!this.Room.CanBeBooked())
            {
                throw new RoomNotAvailableException();
            }
        }
    }
}
