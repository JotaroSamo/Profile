using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace profile_DataAccess.Context
{
    public class ProfileDbContextFactory : IDesignTimeDbContextFactory<ProfileDbContext>
    {
        public ProfileDbContext CreateDbContext(string[] args)
        {
            // replace with local env variables:
            // host, port, database, username and password
            // For example: Host=localhost;Port=5432;Database=Gid;Username=postgres;Password=postgres
            var connection = "Host=localhost;Port=5432;Database=Profile;Username=postgres;Password=postgres";
            var optionsBuilder = new DbContextOptionsBuilder<ProfileDbContext>();

            optionsBuilder.UseNpgsql(connection, opt => opt.CommandTimeout((int) TimeSpan.FromMinutes(2).TotalSeconds));

            return new ProfileDbContext(optionsBuilder.Options);
        }
    }
}