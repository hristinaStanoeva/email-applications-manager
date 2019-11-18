using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using EMS.Services.dto_Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Services.Contracts
{
    public interface IEmailService
    {
        Task ChangeStatusAsync(string id, EmailStatus newStatus);
        Task<List<EmailDto>> GetAllEmailsAsync();       
        Task<string> GetGmailId(string id);
        Task<List<AttachmentDto>> GetAttachmentsAsync(string emailId);
        Task<EmailDto> GetSingleMail(string mailId);
        Task<string> GetBodyAsync(string messageId);
        Task<EmailDto> FindEmailAsync(string id);
        Task<EmailDto> CreateAsync(DateTime received, string gmailMessageId, string senderEmail, string senderName, string subject, List<AttachmentDomain> attachments);
        Task<EmailDto> CreateAsync(DateTime received, string gmailMessageId, string senderEmail, string subject, List<AttachmentDomain> attachments);
        Task AddBodyAsync(string emailId, string body);
        Task<List<AttachmentDto>> CreateAttachmentsAsync();
    }
}
