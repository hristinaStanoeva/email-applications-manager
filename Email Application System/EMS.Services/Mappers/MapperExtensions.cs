﻿using EMS.Data.dbo_Models;
using EMS.Services.dto_Models;
using EMS.Services.Security;
using System.Linq;

namespace EMS.Services.Mappers
{
    public static class MapperExtensions
    {
        public static UserDto MapToDtoModel(this UserDomain user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                CreatedOn = user.CreatedOn,
                IsPasswordChanged = user.IsPasswordChanged
            };
        }

        public static EmailDto MapToDtoModel(this EmailDomain email)
        {
            return new EmailDto
            {
                Id = email.Id,
                Received = email.Received,
                NumberOfAttachments = email.NumberOfAttachments,
                SenderEmail = email.SenderEmail,
                SenderName = email.SenderName,
                Status = email.Status,
                Subject = email.Subject,
                Body = email.Body,
                ToCurrentStatus = email.ToCurrentStatus,
                GmailMessageId = email.GmailMessageId,
                Attachments = email.Attachments.Select(e => e.MapToDtoModel()).ToList()
            };
        }

        public static ApplicationDto MapToDtoModel(this ApplicationDomain app)
        {
            return new ApplicationDto
            {
                Id = app.Id,
                EGN = Encrypt.DecryptData(app.EGN),
                Name = Encrypt.DecryptData(app.Name),
                Status = app.Status,
                PhoneNumber = Encrypt.DecryptData(app.PhoneNumber),
                UserId = app.UserId
            };
        }

        public static AttachmentDto MapToDtoModel(this AttachmentDomain att)
        {
            return new AttachmentDto
            {
                Name = att.Name,
                SizeMb = att.SizeMb
            };
        }
    }
}
