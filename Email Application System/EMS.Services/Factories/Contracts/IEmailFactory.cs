using EMS.Data.dbo_Models;
using System;
using System.Collections.Generic;

namespace EMS.Services.Factories.Contracts
{
    public interface IEmailFactory
    {
        EmailDomain CreateEmail(DateTime received, string gmailMessageId, string senderEmail, string senderName, string subject, List<AttachmentDomain> attachments);
        EmailDomain CreateEmail(DateTime received, string gmailMessageId, string senderEmail, string subject, List<AttachmentDomain> attachments);
    }
}
