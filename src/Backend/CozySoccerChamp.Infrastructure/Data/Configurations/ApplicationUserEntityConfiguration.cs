using CozySoccerChamp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CozySoccerChamp.Infrastructure.Data.Configurations;

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ChatId)
            .IsRequired();

        builder.Property(x => x.UserName)
            .HasMaxLength(50);

        builder.HasIndex(x => x.ChatId)
            .IsUnique();
    }
}