using EMS.Data.dbo_Models;
using EMS.Services.dto_Models;
using EMS.WebProject.Models;
using EMS.WebProject.Models.Applications;
using EMS.WebProject.Models.Emails;
using EMS.WebProject.Parsers;
using System;
using System.Collections.Generic;
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
                DateReceived = email.Received.ToString("dd.MM.yyyy HH:mm")
            };
        }
        public static GenericAppViewModel MapToViewModel(this ApplicationDto app)
        {
            return new GenericAppViewModel
            {
                Id = app.Id.ToString(),                
                Operator = app.User,
                Email = app.Email,
                EGN = app.EGN,
                Name = app.Name,
                Phone = app.PhoneNumber,
                Status = app.Status.ToString()
            };
        }
        public static GenericAppViewModel MapToViewModelOpenMail(this ApplicationDto app)
        {
            return new GenericAppViewModel
            {
                Id = app.Id.ToString(),
                Operator = app.User,
                Email = app.Email,
                EGN = app.EGN,
                Name = app.Name,
                Phone = app.PhoneNumber,
                Status = app.Status.ToString()
            };
        }

        public static AppPreviewViewModel MapToViewModelPreview(this ApplicationDto app, string emailId)
        {
            return new AppPreviewViewModel
            {
                Id = app.Id.ToString(),
                EGN = app.EGN,
                Name = app.Name,
                Phone = app.PhoneNumber,
                Email = app.Email,
                Status = app.Status,
                OperatorName = app.User.Username
            };
        }

        public static GenericEmailViewModel MapToViewModel(this EmailDto email)
        {
            return new GenericEmailViewModel
            {
                Id = email.Id.ToString(),
                DateReceived = email.Received.ToString("dd.MM.yyyy HH:mm"),
                HasAttachments = email.NumberOfAttachments > 0,
                SenderEmail = email.SenderEmail,
                SenderName = email.SenderName,
                Status = email.Status.ToString(),
                Subject = email.Subject,
                EmailBody = email.Body,
                TimeSinceCurrentStatus = TimeSpanParser.StatusParser(email)
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
