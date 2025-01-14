using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using profile_DataAccess.Context.Entity.Chat;

namespace profile_DataAccess.Context.Configuration;

public class ChatEntityConfiguration : IEntityTypeConfiguration<ChatEntity>
{
    public void Configure(EntityTypeBuilder<ChatEntity> builder)
    {
        builder.ToTable("Chats");
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.PublicId)
            .IsRequired();
        
        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(200);
        builder.HasMany(c => c.Users).WithMany(x => x.Chats);
        builder.HasMany(c => c.Messages)
            .WithOne(m => m.ChatEntity)
            .HasForeignKey(m => m.ChatId).HasPrincipalKey(x=>x.PublicId)
            .OnDelete(DeleteBehavior.Cascade); // При удалении чата удалять связанные сообщения
    }
}
