using Microsoft.EntityFrameworkCore;
using profile_DataAccess.Context.Configuration;
using profile_DataAccess.Context.Entity.Chat;
using profile_DataAccess.Context.Entity.Profile;
using profile_Domain.Chat;

namespace profile_DataAccess.Context;

public class ProfileDbContext : DbContext
{
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options): base(options)
        {
        }
          public  DbSet<UserEntity> Users { get; set; }
          public  DbSet<PostEntity> Posts { get; set; }
          
          public DbSet<ChatEntity> Chats { get; set; }
          
          public DbSet<MessageEntity> Messages { get; set; }
          
          public DbSet<UserChatConnectionEntity> UserChatConnections { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostEntityConfiguration).Assembly);
        }
}