using EMS.Data.dbo_Models;
using EMS.Services.dto_Models;
using EMS.WebProject.Models;
using EMS.WebProject.Models.Applications;
using EMS.WebProject.Models.Emails;
using EMS.WebProject.Parsers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebProject.Mappers
{
    public static class MapperExtensions
    {
        public static PreviewViewModel MapToViewModelPreview(this EmailDto email, string body, List<AttachmentViewModel> attachmentsVM)
        {
            return new PreviewViewModel
            {
                Id = email.Id.ToString(),
                SenderEmail = email.SenderEmail,
                SenderName = email.SenderName,
                Subject = email.Subject,
                Status = email.Status.ToString(),
                EmailBody = body,
                Attachments = attachmentsVM
            };
        }

        public static PreviewViewModel MapToViewModelPreview(this EmailDto email)
        {
            return new PreviewViewModel
            {
                Id = email.Id.ToString(),
                SenderEmail = email.SenderEmail,
                SenderName = email.SenderName,
                Subject = email.Subject,
                Status = email.Status.ToString(),
                EmailBody = email.Body,
                DateReceived = email.Received.ToLocalTime().ToString("dd.MM.yyyy HH:mm")
            };
        }

        public static GenericEmailViewModel MapToViewModel(this EmailDto email)
        {
            return new GenericEmailViewModel
            {
                Id = email.Id.ToString(),
                DateReceived = email.Received.ToLocalTime().ToString("dd.MM.yyyy HH:mm"),
                HasAttachments = email.NumberOfAttachments > 0,
                SenderEmail = email.SenderEmail,
                SenderName = email.SenderName,
                Status = email.Status.ToString(),
                Subject = email.Subject,
                TimeSinceCurrentStatus = TimeSpanParser.StatusParser(email),
                MessageId = email.GmailMessageId,
                Attachments = email.Attachments.Select(e => e.MapToViewModel()).ToList()
            };
        }

        public static GenericAppViewModel MapToViewModel(this ApplicationDomain app)
        {
            return new GenericAppViewModel
            {
                Id = app.Id.ToString(),
                EmailDateReceived = app.Email.Received.ToLocalTime().ToString("dd.MM.yyyy HH:mm"),
                SenderEmail = app.Email.SenderEmail,
                Subject = app.Email.Subject,
                SenderName = app.Name,
                Status = app.Status.ToString(),
                ClosedByOperator = app.User.ToString()
            };
        }

        public static AttachmentViewModel MapToViewModel(this AttachmentDto att)
        {
            return new AttachmentViewModel
            {
                Name = att.Name,
                Size = Math.Round(att.SizeMb, 2)
            };
        }
    }
}
