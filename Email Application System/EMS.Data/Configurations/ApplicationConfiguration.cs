using EMS.Data.dbo_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS.Data.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<ApplicationDomain>
    {
        public void Configure(EntityTypeBuilder<ApplicationDomain> builder)
        {
            builder
                .HasKey(app => app.Id);

            builder
                .HasOne(app => app.Email)
                .WithOne(email => email.Application);

            builder
               .HasOne(app => app.User)
               .WithMany(user => user.Applications)
               .HasForeignKey(app => app.UserId);

            builder.ToTable("Applications");
        }
    }
}
