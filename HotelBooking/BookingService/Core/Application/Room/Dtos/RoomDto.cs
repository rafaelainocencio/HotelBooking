using Domain.Guest.Enums;
using RoomEntity = Domain.Room.Entities;
using Domain;

namespace Application.Room.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public decimal Price { get; set; }
        public AcceptedCurrencies Currency { get; set; }

        public static RoomEntity.Room MapToEntity(RoomDto dto)
        {
            return new RoomEntity.Room
            {
                Id = dto.Id,
                Name = dto.Name,
                Level = dto.Level,
                InMaintenance = dto.InMaintenance,
                Price = new Domain.Room.ValueObjects.Price { Currency = dto.Currency, Value = dto.Price }
            };
        }

        public static RoomDto MapToDto(RoomEntity.Room room)
        {
            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Level = room.Level,
                InMaintenance = room.InMaintenance,
                Price = room.Price.Value,
                Currency = room.Price.Currency
            };
        }
    }
}