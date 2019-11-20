using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using EMS.GmailAPI.gmail_Models;
using System;
using System.Linq;

namespace EMS.GmailAPI.Mappers
{
    public static class MapperExtensions
    {
        public static EmailDomain MapToDomainModel(this EmailGmail email)
        {
            return new EmailDomain
            {
                GmailMessageId = email.GmailMessageId,
                Received = email.Received,
                SenderEmail = email.SenderEmail,
                SenderName = email.SenderName,
                Subject = email.Subject,
                NumberOfAttachments = email.Attachments.Count,
                SizeOfAttachmentsMb = email.Attachments.Sum(att => att.SizeMb),
                Status = EmailStatus.NotReviewed,
                ToCurrentStatus = DateTime.UtcNow
            };
        }

        public static AttachmentDomain MapToDomainModel(this AttachmentGmail attachment)
        {
            return new AttachmentDomain
            {
                Name = attachment.Name,
                SizeMb = attachment.SizeMb
            };
        }
    }
}
