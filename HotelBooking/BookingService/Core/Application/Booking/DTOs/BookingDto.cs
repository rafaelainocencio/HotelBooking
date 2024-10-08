using Domain.Guest.Enums;

namespace Application.Booking.DTOs
{
    public class BookingDto
    {

        public BookingDto()
        {
            this.PlacedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Status Status { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }

        public static Domain.Booking.Entities.Booking MapToEntity(BookingDto dto)
        {
            return new Domain.Booking.Entities.Booking
            {
                Id = dto.Id,
                PlacedAt = dto.PlacedAt,
                Start = dto.Start,
                Room = new Domain.Room.Entities.Room { Id = dto.RoomId},
                Guest = new Domain.Guest.Entities.Guest { Id = dto.RoomId },
                End = dto.End,
            };
        }

        public static BookingDto MapToDto(Domain.Booking.Entities.Booking booking)
        {
            return new BookingDto
            {
                PlacedAt = booking.PlacedAt,
                Start = booking.Start,
                End = booking.End,
                Status = booking.Status,
                RoomId = booking.Room.Id,
                GuestId = booking.Guest.Id,
            };
        }
    }
}
