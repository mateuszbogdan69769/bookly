using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Roots.Statuses;

namespace Infrastructure.Persistence.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> entity)
    {
        entity.ToTable("status");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id).HasColumnType("integer");

        entity.Property(e => e.Name).HasColumnType("text");

        entity.Property(e => e.Color).HasColumnType("text");

        entity.Property(e => e.Order).HasColumnType("integer");

        entity.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");
    }
}
