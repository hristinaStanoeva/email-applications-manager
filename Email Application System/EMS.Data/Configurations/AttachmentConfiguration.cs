using EMS.Data.dbo_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS.Data.Configurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<AttachmentDomain>
    {
        public void Configure(EntityTypeBuilder<AttachmentDomain> builder)
        {
            builder
                .HasKey(att => att.Id);

            builder
                .HasOne(att => att.Email)
                .WithMany(email => email.Attachments)
                .HasForeignKey(att => att.EmailId);

            builder.ToTable("Attachments");
        }
    }
}
