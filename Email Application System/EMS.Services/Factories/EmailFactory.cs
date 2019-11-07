using EMS.Data.dbo_Models;
using EMS.Services.Factories.Contracts;
using System;
using System.Collections.Generic;

namespace EMS.Services.Factories
{
    public class EmailFactory : IEmailFactory
    {
        public EmailDomain CreateEmail(DateTime received, string gmailMessageId, string senderEmail, string senderName, string subject, List<AttachmentDomain> attachments)
        {
            throw new NotImplementedException();
        }

        public EmailDomain CreateEmail(DateTime received, string gmailMessageId, string senderEmail, string subject, List<AttachmentDomain> attachments)
        {
            throw new NotImplementedException();
        }
    }
}
