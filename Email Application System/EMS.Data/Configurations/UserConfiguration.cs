using EMS.Data.dbo_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserDomain>
    {
        public void Configure(EntityTypeBuilder<UserDomain> builder)
        {
            builder
               .HasKey(user => user.Id);

            builder
               .HasMany(user => user.Applications)
               .WithOne(app => app.User);

            builder
              .HasMany(user => user.Logs)
              .WithOne(log => log.User);

            builder.ToTable("Users");
        }
    }
}
