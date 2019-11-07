using EMS.Data.dbo_Models;
using EMS.Services.Factories.Contracts;
using System;
using System.Collections.Generic;

namespace EMS.Services.Factories
{
    public class EmailFactory : IEmailFactory
    {
        public DboEmail CreateEmail(DateTime received, string gmailMessageId, string senderEmail, string senderName, string subject, List<DboAttachment> attachments)
        {
            throw new NotImplementedException();
        }

        public DboEmail CreateEmail(DateTime received, string gmailMessageId, string senderEmail, string subject, List<DboAttachment> attachments)
        {
            throw new NotImplementedException();
        }
    }
}
