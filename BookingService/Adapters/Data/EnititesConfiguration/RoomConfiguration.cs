using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EnititesConfiguration;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.Price)
            .Property(p => p.Value);
        builder.OwnsOne(x => x.Price)
            .Property(p => p.Currency);
    }
}
