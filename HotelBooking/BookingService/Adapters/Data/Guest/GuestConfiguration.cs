﻿using Microsoft.EntityFrameworkCore;

namespace Data.Guest
{
    public class GuestConfiguration : IEntityTypeConfiguration<Domain.Guest.Entities.Guest>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Guest.Entities.Guest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.DocumentId)
                .Property(x => x.IdNumber);

            builder.OwnsOne(x => x.DocumentId)
                .Property(x => x.DocumentType);
        }
    }
}
