using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Roots.Guests;

namespace Infrastructure.Persistence.Configurations;

public class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> entity)
    {
        entity.ToTable("guest");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id).HasColumnType("integer");

        entity.Property(e => e.Name).HasColumnType("text");

        entity.Property(e => e.Surname).HasColumnType("text");

        entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone");
    }
}
