using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using profile_DataAccess.Context.Entity.Chat;

namespace profile_DataAccess.Context.Configuration;

public class UserChatConnectionEntityConfiguration : IEntityTypeConfiguration<UserChatConnectionEntity>
{
    public void Configure(EntityTypeBuilder<UserChatConnectionEntity> builder)
    {
        builder.ToTable("UserChatConnections"); // Имя таблицы в базе данных
        builder.HasKey(uc => uc.Id); // Установка первичного ключа

        builder.Property(uc => uc.ConnectionId)
            .IsRequired(); // ConnectionId обязательное поле

        builder.Property(uc => uc.UserId)
            .IsRequired(); // UserId обязательное поле
        
        builder.HasOne(uc => uc.User) // Настройка связи с сущностью UserEntity
            .WithMany(u=>u.Connections) 
            .HasForeignKey(uc => uc.UserId).HasPrincipalKey(x=>x.PublicId) // Установка внешнего ключа
            .OnDelete(DeleteBehavior.Cascade); 
        
        builder.HasOne(uc => uc.Chat) 
            .WithMany() 
            .HasForeignKey(uc => uc.ChatId).HasPrincipalKey(x=>x.PublicId) 
            .OnDelete(DeleteBehavior.Cascade); 
    }
}