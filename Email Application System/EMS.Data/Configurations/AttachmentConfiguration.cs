using EMS.Data.dbo_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS.Data.Configurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<DboAttachment>
    {
        public void Configure(EntityTypeBuilder<DboAttachment> builder)
        {
            builder
                .HasKey(att => att.Id);

            builder
                .HasOne(att => att.Email)
                .WithMany(email => email.Attachments)
                .HasForeignKey(att => att.EmailId);
        }
    }
}
