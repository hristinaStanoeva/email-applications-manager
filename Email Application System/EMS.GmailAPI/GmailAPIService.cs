using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestGmailAPI
{
    public static class GmailAPIService
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";

        public static void GmailSync()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var emailsListRequest = service.Users.Messages.List("application.southwest@gmail.com");

            emailsListRequest.LabelIds = "INBOX";
            emailsListRequest.IncludeSpamTrash = false;

            // This was added because I only wanted unread emails...
            //emailsListRequest.Q = "is:unread"; 

            // Get our emails
            var emailsListResponse = emailsListRequest.ExecuteAsync().Result;

            // Loop through each email and get what fields you want...
            foreach (var email in emailsListResponse.Messages)
            {
                var emailInfoRequest = service.Users.Messages.Get("application.southwest@gmail.com", email.Id);
                // Make another request for that email id...
                var emailInfoResponse = emailInfoRequest.ExecuteAsync().Result;

                var sentBy = emailInfoResponse.Payload.Headers.FirstOrDefault(header => header.Name == "From").Value;
                var subject = emailInfoResponse.Payload.Headers.FirstOrDefault(header => header.Name == "Subject").Value;
                var sentReceived = emailInfoResponse.Payload.Headers.FirstOrDefault(header => header.Name == "Date").Value;
                var emailId = emailInfoResponse.Payload.Headers.FirstOrDefault(header => header.Name == "Message-ID").Value;

                if (emailInfoResponse.Payload.Parts[0].MimeType=="text/plain")
                {
                    var bodyPlainTextEncrypted = emailInfoResponse.Payload.Parts.FirstOrDefault(part => part.MimeType == "text/plain").Body.Data;
                    var bodyHtmlTextEncrypted = emailInfoResponse.Payload.Parts.FirstOrDefault(part => part.MimeType == "text/html").Body.Data;

                    String plainTextEncrypted = bodyPlainTextEncrypted.Replace("-", "+");
                    plainTextEncrypted = plainTextEncrypted.Replace("_", "/");
                    byte[] dataPlainText = Convert.FromBase64String(plainTextEncrypted);
                    var plainText = Encoding.UTF8.GetString(dataPlainText);

                    String htmlTextEncrypted = bodyHtmlTextEncrypted.Replace("-", "+");
                    htmlTextEncrypted = htmlTextEncrypted.Replace("_", "/");
                    byte[] dataHtmlText = Convert.FromBase64String(htmlTextEncrypted);
                    var htmlText = Encoding.UTF8.GetString(dataHtmlText);
                }
                else
                {
                    var bodyPlainTextEncrypted = emailInfoResponse.Payload.Parts[0].Parts.FirstOrDefault(part => part.MimeType == "text/plain").Body.Data;
                    var bodyHtmlTextEncrypted = emailInfoResponse.Payload.Parts[0].Parts.FirstOrDefault(part => part.MimeType == "text/html").Body.Data;

                    String plainTextEncrypted = bodyPlainTextEncrypted.Replace("-", "+");
                    plainTextEncrypted = plainTextEncrypted.Replace("_", "/");
                    byte[] dataPlainText = Convert.FromBase64String(plainTextEncrypted);
                    var plainText = Encoding.UTF8.GetString(dataPlainText);

                    String htmlTextEncrypted = bodyHtmlTextEncrypted.Replace("-", "+");
                    htmlTextEncrypted = htmlTextEncrypted.Replace("_", "/");
                    byte[] dataHtmlText = Convert.FromBase64String(htmlTextEncrypted);
                    var htmlText = Encoding.UTF8.GetString(dataHtmlText);

                    var attachmentsInfoReceived = emailInfoResponse.Payload.Parts.Skip(1);
                    
                    foreach (var attachment in attachmentsInfoReceived)
                    {
                        var attachmentName = attachment.Filename;
                        var attachmentSizeBytes = attachment.Body.Size;
                    }
                }

            }
        }
    }
}
