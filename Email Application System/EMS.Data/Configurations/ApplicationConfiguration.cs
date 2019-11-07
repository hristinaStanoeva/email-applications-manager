using EMS.Data.dbo_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS.Data.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<DboApplication>
    {
        public void Configure(EntityTypeBuilder<DboApplication> builder)
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

           // builder.ToTable("Applications");
        }
    }
}
