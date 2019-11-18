using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.Services.dto_Models;
using EMS.Services.Mappers;
using GmailAPI;
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
        private readonly IGmailAPIService _gmailService;

        public EmailService(SystemDataContext context, IGmailAPIService gmailService)
        {
            _context = context;
            _gmailService = gmailService;
        }

        public async Task<List<EmailDto>> GetAllEmailsAsync()
        {
            var emailsDomain = await _context.Emails
                .Include(email=>email.Attachments)
                .ToListAsync().ConfigureAwait(false);

            var emailsDto = new List<EmailDto>();

            foreach (var email in emailsDomain)
            {
                emailsDto.Add(email.MapToDtoModel());
            }

            return emailsDto;
        }

        public async Task<EmailDto> GetSingleMail(string mailId)
        {
            var emailDomain = await _context.Emails
                .FirstOrDefaultAsync(mail => mail.Id.ToString() == mailId)
                .ConfigureAwait(false);

            return emailDomain.MapToDtoModel();
        }
        public async Task<string> GetGmailId(string id)
        {
            var mail = await _context.Emails
                .FirstOrDefaultAsync(email => email.Id.ToString() == id)
                .ConfigureAwait(false);

            return mail.GmailMessageId;
        }
        public async Task<List<AttachmentDto>> GetAttachmentsAsync(string emailId)
        {
            var attachmentsDomain = await _context.Attachments
                .Where(att => att.EmailId.ToString() == emailId)
                .ToListAsync().ConfigureAwait(false);

            if (attachmentsDomain.Count == 0)
                return null;

            var attachmentsDto = new List<AttachmentDto>();
            foreach (var att in attachmentsDomain)
            {
                attachmentsDto.Add(att.MapToDtoModel());
            }

            return attachmentsDto;
        }             
        
        public async Task AddBodyAsync(string emailId, string body)
        {
            throw new NotImplementedException();
        }

        public async Task ChangeStatusAsync(string id, EmailStatus newStatus)
        {
            var email = await _context.Emails
                .FirstOrDefaultAsync(mail => mail.Id.ToString() == id)
                .ConfigureAwait(false);

            email.Status = newStatus;

            await _context.SaveChangesAsync().ConfigureAwait(false);
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

        public async Task<string> GetBodyAsync(string messageId)
        {
            return await _gmailService.GetEmailBodyAsync(messageId);
        }
    }
}
