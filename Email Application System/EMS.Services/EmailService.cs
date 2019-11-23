using EMS.Data;
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
                .Include(email => email.Attachments)
                .OrderByDescending(email => email.Received)
                .ToListAsync()
                .ConfigureAwait(false);

            var emailsDto = new List<EmailDto>();
            foreach (var email in emailsDomain)
            {
                emailsDto.Add(email.MapToDtoModel());
            }

            return emailsDto;
        }
        public async Task<EmailDto> GetSingleEmailAsync(string mailId)
        {
            var emailDomain = await _context.Emails
                .Include(email => email.Attachments)
                .FirstOrDefaultAsync(mail => mail.Id.ToString() == mailId)
                .ConfigureAwait(false);

            return emailDomain.MapToDtoModel();
        }
        public async Task<string> GetGmailIdAsync(string id)
        {
            var mail = await _context.Emails
                .FirstOrDefaultAsync(email => email.Id.ToString() == id)
                .ConfigureAwait(false);

            return mail.GmailMessageId;
        }
        public async Task<List<EmailDto>> GetNewEmailsAsync()
        {
            var emailsDomain = await _context.Emails
                .Where(mail => mail.Status == EmailStatus.New)
                .Include(mail => mail.Attachments)
                .OrderByDescending(mail => mail.ToNewStatus)
                .ToListAsync()
                .ConfigureAwait(false);

            var emailsDto = new List<EmailDto>();
            foreach (var email in emailsDomain)
            {
                emailsDto.Add(email.MapToDtoModel());
            }

            return emailsDto;
        }
        public async Task<List<EmailDto>> GetOpenEmailsAsync()
        {
            var emailsDomain = await _context.Emails
                .Where(mail => mail.Status == EmailStatus.Open)
                .Include(mail => mail.Attachments)
                .OrderByDescending(mail => mail.ToCurrentStatus)
                .ToListAsync()
                .ConfigureAwait(false);

            var emailsDto = new List<EmailDto>();
            foreach (var email in emailsDomain)
            {
                emailsDto.Add(email.MapToDtoModel());
            }

            return emailsDto;
        }
        public async Task<List<EmailDto>> GetClosedEmailsAsync()
        {
            var emailsDomain = await _context.Emails
                .Where(mail => mail.Status == EmailStatus.Closed)
                .Include(mail => mail.Attachments)
                .ToListAsync()
                .ConfigureAwait(false);

            var emailsDto = new List<EmailDto>();
            foreach (var email in emailsDomain)
            {
                emailsDto.Add(email.MapToDtoModel());
            }

            return emailsDto;
        }
        public async Task ChangeStatusAsync(string id, EmailStatus newStatus)
        {
            var email = await _context.Emails
                .FirstOrDefaultAsync(mail => mail.Id.ToString() == id)
                .ConfigureAwait(false);

            if (newStatus == EmailStatus.New)
            {
                email.ToNewStatus = DateTime.UtcNow;
                if (email.Status == EmailStatus.NotReviewed)
                {
                    await this.AddBodyAsync(email.Id);
                }
            }
            else if (newStatus == EmailStatus.Closed)
            {
                email.ToTerminalStatus = DateTime.UtcNow;
            }
            email.ToCurrentStatus = DateTime.UtcNow;
            email.Status = newStatus;

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task<string> GetBodyByGmailAsync(string messageId)
        {
            return await _gmailService.GetEmailBodyAsync(messageId);
        }
        public async Task<string> GetBodyByDbAsync(string emailId)
        {
            var email = await _context.Emails.FirstOrDefaultAsync(e => e.Id.ToString() == emailId);

            if (email.Body is null)
            {
                return Constants.NoBody;
            }
            else return _gmailService.Decrypt(email.Body);
        }
        private async Task AddBodyAsync(Guid emailId)
        {
            var email = await _context.Emails
                .FirstOrDefaultAsync(mail => mail.Id == emailId)
                .ConfigureAwait(false);

            var encryptedBody = await _gmailService.GetEncryptedBodyAsync(email.GmailMessageId);

            if (encryptedBody is null)
            {
                throw new ArgumentNullException("Error occured - email body is not foung");
            }
            else
            {
                email.Body = encryptedBody;
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
