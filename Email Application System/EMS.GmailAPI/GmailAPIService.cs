using EMS.Data;
using EMS.GmailAPI.gmail_Models;
using EMS.GmailAPI.Mappers;
using EMS.GmailAPI.Parsers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GmailAPI
{
    public class GmailAPIService : IGmailAPIService
    {
        // If modifying these scopes, delete your previously saved credentials at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailModify };
        static string ApplicationName = "Gmail API";
        static string EmailAddress = "application.southwest@gmail.com";
        private readonly SystemDataContext _context;

        public GmailAPIService(SystemDataContext context)
        {
            _context = context;
        }


        public async Task<string> GetEmailBody(string emailId)
        {
            // Create Gmail API service.
            var service = await CreateGmailService();

            var email1 = await GetEmailInfo(service, emailId);
            var email = email1.Payload;

            string bodyPlainTextEncrypted = string.Empty;
            // string bodyHtmlTextEncrypted = string.Empty;

            if (email.Parts[0].MimeType == "text/plain")
            {
                bodyPlainTextEncrypted = email.Parts.FirstOrDefault(part => part.MimeType == "text/plain").Body.Data;
                // bodyHtmlTextEncrypted = email.Parts.FirstOrDefault(part => part.MimeType == "text/html").Body.Data;
            }
            else
            {
                bodyPlainTextEncrypted = email.Parts[0].Parts.FirstOrDefault(part => part.MimeType == "text/plain").Body.Data;
                //bodyHtmlTextEncrypted = email.Parts[0].Parts.FirstOrDefault(part => part.MimeType == "text/html").Body.Data;
            }

            return Decrypt(bodyPlainTextEncrypted);
        }

        public async Task GmailSync()
        {
            // Create Gmail API service.
            var service = await CreateGmailService();
            // Get all emails
            var emails = await GetEmails(service);

            if (emails is null)
            {
                return;
            }

            var parsedEmails = new List<EmailGmail>();

            // Loop through each email
            foreach (var email in emails)
            {
                var emailId = email.Id;

                var emailInfo = await GetEmailInfo(service, emailId);

                var parsedEmail = CreateEmail(emailInfo.Payload, emailInfo.InternalDate ?? throw new ArgumentNullException());
                parsedEmail.GmailMessageId = emailId;

                parsedEmails.Add(parsedEmail);
            }

            await AddToContext(parsedEmails).ConfigureAwait(false);
        }

        private async Task AddToContext(List<EmailGmail> gmailEmails)
        {
            foreach (var email in gmailEmails)
            {
                var newDomainEmail = email.MapToDomainModel();

                var domainEmail = _context.Emails.Add(newDomainEmail);

                if (email.Attachments.Count != 0)
                {
                    foreach (var attachment in email.Attachments)
                    {
                        var newDomainAttachment = attachment.MapToDomainModel();
                        newDomainAttachment.EmailId = domainEmail.Entity.Id;

                        _context.Attachments.Add(newDomainAttachment);
                    }
                }

            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }


        private async Task<GmailService> CreateGmailService()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            return new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }
        private async Task<IList<Message>> GetEmails(GmailService service)
        {
            var emailsListRequest = service.Users.Messages.List(EmailAddress);

            emailsListRequest.LabelIds = "INBOX";
            emailsListRequest.IncludeSpamTrash = false;
            // Unread emails
            emailsListRequest.Q = "is:unread";

            var emailsListResponse = emailsListRequest.ExecuteAsync().Result;
            return emailsListResponse.Messages;
        }
        private async Task<Message> GetEmailInfo(GmailService service, string emailId)
        {
            var emailInfoRequest = service.Users.Messages.Get(EmailAddress, emailId);
            var emailInfoResponse = await emailInfoRequest.ExecuteAsync();

            var markAsReadRequest = new ModifyMessageRequest { RemoveLabelIds = new List<string> { "UNREAD" } };

            await service.Users.Messages.Modify(markAsReadRequest, "me", emailInfoResponse.Id).ExecuteAsync();

            return emailInfoResponse;
        }
        private EmailGmail CreateEmail(MessagePart emailInfo, long internalDate)
        {
            var senderEmail = DataParser.ParseSenderEmail(emailInfo.Headers.FirstOrDefault(header => header.Name == "From").Value);
            var senderName = DataParser.ParseSenderName(emailInfo.Headers.FirstOrDefault(header => header.Name == "From").Value);
            var subject = emailInfo.Headers.FirstOrDefault(header => header.Name == "Subject").Value;
            var dateReceived = DataParser.ParseDate(internalDate);
            var attachmentsList = new List<AttachmentGmail>();

            if (emailInfo.Parts[0].MimeType != "text/plain")
            {
                var attachmentsInfoReceived = emailInfo.Parts.Skip(1);

                foreach (var attachment in attachmentsInfoReceived)
                {
                    var sizeMb = (double)attachment.Body.Size / (1024 * 1024);

                    var newAttachment = new AttachmentGmail
                    {
                        Name = attachment.Filename,
                        SizeMb = sizeMb
                    };

                    attachmentsList.Add(newAttachment);
                }
            }

            if (emailInfo.Parts[1].MimeType == "multipart/related")
            {
                var attachmentsInfoReceived = emailInfo.Parts[1].Parts.Skip(1);

                foreach (var attachment in attachmentsInfoReceived)
                {
                    var sizeMb = (double)attachment.Body.Size / (1024 * 1024);

                    var newAttachment = new AttachmentGmail
                    {
                        Name = attachment.Filename,
                        SizeMb = sizeMb
                    };

                    attachmentsList.Add(newAttachment);
                }
            }

            return new EmailGmail
            {
                Received = dateReceived,
                SenderEmail = senderEmail,
                SenderName = senderName,
                Subject = subject,
                Attachments = attachmentsList
            };
        }
        private string Decrypt(string encryptedText)
        {
            var tempText = encryptedText.Replace("-", "+");
            tempText = tempText.Replace("_", "/");
            byte[] dataText = Convert.FromBase64String(tempText);
            return Encoding.UTF8.GetString(dataText);
        }
    }
}
