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

       public static GenericAppViewModel MapToViewModel(this ApplicationDomain app)
        {
            return new GenericAppViewModel
            {
                Id = app.Id.ToString(),
                EmailDateReceived = app.Email.Received.ToString("dd.MM.yyyy HH:mm"),
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
