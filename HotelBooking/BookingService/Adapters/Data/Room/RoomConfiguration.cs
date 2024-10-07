using Domain.Room.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomEntity = Domain.Room.Entities;

namespace Data.Room
{
    public class RoomConfiguration : IEntityTypeConfiguration<RoomEntity.Room>
    {
        public void Configure(EntityTypeBuilder<RoomEntity.Room> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Price)
                .Property(x => x.Currency);

            builder.OwnsOne(x => x.Price)
                .Property(x => x.Value)
                .HasColumnType("decimal(5,2)");
        }
    }
}
