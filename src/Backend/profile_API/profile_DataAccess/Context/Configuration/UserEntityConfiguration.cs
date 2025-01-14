using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using profile_DataAccess.Context.Entity.Profile;

namespace profile_DataAccess.Context.Configuration;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("User");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Login).IsRequired().HasMaxLength(100);
        builder.HasIndex(l => l.Login).IsUnique();
        builder.Property(u => u.PublicId).IsRequired();
        builder.HasIndex(l => l.PublicId).IsUnique();
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.AvatarUrl).HasMaxLength(200);
        builder.Property(u => u.HasPassword).IsRequired();
        builder.Property(u => u.Salt).IsRequired();

        builder.HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .HasPrincipalKey(u => u.PublicId);
        builder.HasMany(c => c.Chats).WithMany(x => x.Users);

    }
}
