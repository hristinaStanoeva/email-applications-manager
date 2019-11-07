using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.Services.dto_Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Services
{
    public class EmailService : IEmailService
    {
        private readonly SystemDataContext _context;

        public EmailService(SystemDataContext context)
        {
            _context = context;
        }

        public Task AddBodyAsync(string emailId, string body)
        {
            throw new NotImplementedException();
        }

        public Task ChangeStatusAsync(string id, EmailStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public Task<DtoEmail> CreateAsync(DateTime received, string gmailMessageId, string senderEmail, string senderName, string subject, List<DboAttachment> attachments)
        {
            throw new NotImplementedException();
        }

        public Task<DtoEmail> CreateAsync(DateTime received, string gmailMessageId, string senderEmail, string subject, List<DboAttachment> attachments)
        {
            throw new NotImplementedException();
        }

        public Task<List<DtoAttachment>> CreateAttachmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DtoEmail> FindEmailAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetBodyAsync(string emailId)
        {
            throw new NotImplementedException();
        }
    }
}
