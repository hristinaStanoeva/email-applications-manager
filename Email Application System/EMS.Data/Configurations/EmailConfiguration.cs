using EMS.Data.dbo_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS.Data.Configurations
{
    public class EmailConfiguration : IEntityTypeConfiguration<DboEmail>
    {
        public void Configure(EntityTypeBuilder<DboEmail> builder)
        {
            builder
                .HasKey(email => email.Id);

            builder
                .HasOne(email => email.Application)
                .WithOne(app => app.Email);

            builder
                .HasMany(email => email.Attachments)
                .WithOne(att => att.Email);

            builder
                .HasMany(email => email.Logs)
                .WithOne(log => log.Email);
        }
    }
}
