using EMS.Data.dbo_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS.Data.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<DboLog>
    {
        public void Configure(EntityTypeBuilder<DboLog> builder)
        {
            builder
               .HasKey(log => log.Id);

            builder
                .HasOne(log => log.User)
                .WithMany(email => email.Logs)
                .HasForeignKey(log => log.UserId);

            builder
               .HasOne(log => log.Email)
               .WithMany(email => email.Logs)
               .HasForeignKey(log => log.EmailId);
        }
    }
}
