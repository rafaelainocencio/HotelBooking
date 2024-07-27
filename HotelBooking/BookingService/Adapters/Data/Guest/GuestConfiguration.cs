using Entities = Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Guest
{
    public class GuestConfiguration : IEntityTypeConfiguration<Entities.Guest>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Guest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.DocumentId)
                .Property(x => x.IdNumber);

            builder.OwnsOne(x => x.DocumentId)
                .Property(x => x.DocumentType);
        }
    }
}
