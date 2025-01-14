using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using profile_DataAccess.Context.Entity.Profile;

namespace profile_DataAccess.Context.Configuration;

public class PostEntityConfiguration : IEntityTypeConfiguration<PostEntity>
{
    public void Configure(EntityTypeBuilder<PostEntity> builder)
    {
        builder.ToTable("Post");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(u => u.PublicId).IsRequired();
        builder.HasIndex(l => l.PublicId).IsUnique();
        builder.Property(p => p.Content).IsRequired();
        builder.Property(p => p.Created).IsRequired();
        builder.HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId).HasPrincipalKey(u => u.PublicId);;
        builder.Property(p => p.Tags);
    }
}
