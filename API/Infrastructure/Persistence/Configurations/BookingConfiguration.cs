using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Roots.Bookings;

namespace Infrastructure.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> entity)
    {
        entity.ToTable("booking");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id).HasColumnType("integer");

        entity.Property(e => e.StartDate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp with time zone");

        entity.Property(e => e.EndDate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp with time zone");

        entity.Property(e => e.GuestId).HasColumnType("integer");

        entity.Property(e => e.PartySize).HasColumnType("integer");

        entity.Property(e => e.Note).HasColumnType("text");

        entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone");

        entity.HasOne(x => x.Guest)
              .WithMany(x => x.Bookings)
              .HasForeignKey(x => x.GuestId)
              .OnDelete(DeleteBehavior.Cascade)
              .HasConstraintName("fk_guest_booking");

        entity.HasOne(x => x.Status)
              .WithMany(x => x.Bookings)
              .HasForeignKey(x => x.StatusId)
              .OnDelete(DeleteBehavior.SetNull)
              .HasConstraintName("fk_status_booking");
    }
}
