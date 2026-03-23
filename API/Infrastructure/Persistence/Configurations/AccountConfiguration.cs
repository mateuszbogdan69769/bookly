using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Roots.Accounts;

namespace Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> entity)
    {
        entity.ToTable("account");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id).HasColumnType("integer");

        entity.Property(e => e.ExternalId).HasMaxLength(36);

        entity.Property(e => e.Username).HasColumnType("text");
        entity.HasIndex(e => e.Username).IsUnique();

        entity.Property(e => e.Name).HasColumnType("text");
    }
}
