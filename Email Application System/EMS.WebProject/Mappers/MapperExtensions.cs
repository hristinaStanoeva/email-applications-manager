using EMS.Data.dbo_Models;
using EMS.WebProject.Models.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebProject.Mappers
{
    public static class MapperExtensions
    {
        public static GenericEmailViewModel MapToViewModel(this EmailDomain email)
        {
            var statusChangeMinutes = DateTime.UtcNow - email.ToCurrentStatus;

            return new GenericEmailViewModel
            {
                Id = email.Id.ToString(),
                DateReceived = email.Received.ToString("dd:MM:yyyy HH:mm"),
                HasAttachments = email.NumberOfAttachments > 0,
                SenderEmail = email.SenderEmail,
                SenderName = email.SenderName,
                Status = email.Status.ToString(),
                Subject = email.Subject,
                TimeSinceCurrentStatus = statusChangeMinutes.Value.Minutes
            };
        }
    }
}
