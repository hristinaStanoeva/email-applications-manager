using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.Services.dto_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<EmailDomain>> GetAllEmailsAsync()
        {
            return await _context.Emails.ToListAsync().ConfigureAwait(false);
        }

        public async Task MakeInvalidAsync(string emailId)
        {
            var email = await _context.Emails
                .FirstOrDefaultAsync(mail => mail.Id.ToString() == emailId)
                .ConfigureAwait(false);
            email.Status = EmailStatus.Invalid;

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task RestoreInvalidAsync(string emailId)
        {
            var email = await _context.Emails
                .FirstOrDefaultAsync(mail => mail.Id.ToString() == emailId)
                .ConfigureAwait(false);
            email.Status = EmailStatus.NotReviewed;

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task AddBodyAsync(string emailId, string body)
        {
            throw new NotImplementedException();
        }

        public Task ChangeStatusAsync(string id, EmailStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public Task<EmailDto> CreateAsync(DateTime received, string gmailMessageId, string senderEmail, string senderName, string subject, List<AttachmentDomain> attachments)
        {
            throw new NotImplementedException();
        }

        public Task<EmailDto> CreateAsync(DateTime received, string gmailMessageId, string senderEmail, string subject, List<AttachmentDomain> attachments)
        {
            throw new NotImplementedException();
        }

        public Task<List<AttachmentDto>> CreateAttachmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmailDto> FindEmailAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetBodyAsync(string emailId)
        {
            throw new NotImplementedException();
        }
    }
}
