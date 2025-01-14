using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using profile_DataAccess.Context.Entity.Chat;

namespace profile_DataAccess.Context.Configuration;

public class MessageEntityConfiguration : IEntityTypeConfiguration<MessageEntity>
{
    public void Configure(EntityTypeBuilder<MessageEntity> builder)
    {
        builder.ToTable("Messages");
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.PublicId)
            .IsRequired();
        
        builder.Property(m => m.Content)
            .IsRequired()
            .HasMaxLength(1000);
        
        builder.Property(m => m.Timestamp)
            .IsRequired();
        
        builder.HasOne(m => m.ChatEntity)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatId).HasForeignKey(x=>x.PublicId)
            .OnDelete(DeleteBehavior.Cascade); // При удалении чата удалять связанные сообщения
    }
}