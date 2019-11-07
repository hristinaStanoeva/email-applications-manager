using EMS.GmailAPI.gmail_Models;
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

namespace TestGmailAPI
{
    public static class GmailAPIService
    {
        // If modifying these scopes, delete your previously saved credentials at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API";
        static string EmailAddress = "application.southwest@gmail.com";

        private static GmailService CreateGmailService()
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
        private static IList<Message> GetEmails(GmailService service)
        {
            var emailsListRequest = service.Users.Messages.List(EmailAddress);

            emailsListRequest.LabelIds = "INBOX";
            emailsListRequest.IncludeSpamTrash = false;
            // Unread emails
            //emailsListRequest.Q = "is:unread"; 

            var emailsListResponse = emailsListRequest.ExecuteAsync().Result;
            return emailsListResponse.Messages;
        }
        private static MessagePart GetEmailInfo(GmailService service, string emailId)
        {
            var emailInfoRequest = service.Users.Messages.Get(EmailAddress, emailId);
            var emailInfoResponse = emailInfoRequest.ExecuteAsync().Result;

            return emailInfoResponse.Payload;
        }
        private static EmailGmail CreateEmail(MessagePart emailInfo)
        {
            var senderEmail = DataParser.ParseSenderEmail(emailInfo.Headers.FirstOrDefault(header => header.Name == "From").Value);
            var senderName = DataParser.ParseSenderName(emailInfo.Headers.FirstOrDefault(header => header.Name == "From").Value);
            var subject = emailInfo.Headers.FirstOrDefault(header => header.Name == "Subject").Value;
            var dateReceived = DataParser.ParseDate(emailInfo.Headers.FirstOrDefault(header => header.Name == "Date").Value);
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
            

            //if (emailInfo.Parts[0].MimeType == "text/plain")
            //{
            //    bodyPlainTextEncrypted = emailInfo.Parts.FirstOrDefault(part => part.MimeType == "text/plain").Body.Data;
            //    bodyHtmlTextEncrypted = emailInfo.Parts.FirstOrDefault(part => part.MimeType == "text/html").Body.Data;
            //}
            //else
            //{
            //    bodyPlainTextEncrypted = emailInfo.Parts[0].Parts.FirstOrDefault(part => part.MimeType == "text/plain").Body.Data;
            //    bodyHtmlTextEncrypted = emailInfo.Parts[0].Parts.FirstOrDefault(part => part.MimeType == "text/html").Body.Data;

            //    var attachmentsInfoReceived = emailInfo.Parts.Skip(1);

            //    foreach (var attachment in attachmentsInfoReceived)
            //    {
            //        var sizeMb = (double)attachment.Body.Size / (1024 * 1024);

            //        var newAttachment = new AttachmentGmail
            //        {
            //            Name = attachment.Filename,
            //            SizeMb = sizeMb
            //        };

            //        attachmentsList.Add(newAttachment);
            //  }
            //}

            return new EmailGmail
            {
                Received = dateReceived,
                SenderEmail = senderEmail,
                SenderName = senderName,
                Subject = subject,
                Attachments = attachmentsList
            };
        }

        private static string Decrypt(string encryptedText)
        {
            var tempText = encryptedText.Replace("-", "+");
            tempText = tempText.Replace("_", "/");
            byte[] dataText = Convert.FromBase64String(tempText);
            return Encoding.UTF8.GetString(dataText);
        }

        public static void GetEmailBody(string emailId)
        {
            // Create Gmail API service.
            var service = CreateGmailService();

            var email = GetEmailInfo(service, emailId);

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

            var plaintext = Decrypt(bodyPlainTextEncrypted);
        }



        public static void GmailSync()
        {
            // Create Gmail API service.
            var service = CreateGmailService();
            // Get all emails
            var emails = GetEmails(service);

            var parsedEmails = new List<EmailGmail>();

            // Loop through each email
            foreach (var email in emails)
            {
                var emailId = email.Id;

                var emailInfo = GetEmailInfo(service, emailId);

                var parsedEmail = CreateEmail(emailInfo);
                parsedEmail.GmailMessageId = emailId;

                parsedEmails.Add(parsedEmail);
            }

            // 16e26aabdc04059d
            // return parsedEmails;
        }
    }
}
