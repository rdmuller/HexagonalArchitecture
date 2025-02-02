using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EnititesConfiguration;
public class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.DocumentId)
            .Property(p => p.IdNumber);
        builder.OwnsOne(x => x.DocumentId)
            .Property(p => p.PersonIdType);
    }
}
