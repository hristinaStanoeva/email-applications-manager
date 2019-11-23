using System.Threading.Tasks;

namespace GmailAPI
{
    public interface IGmailAPIService
    {
        string Decrypt(string encryptedText);
        Task<string> GetEmailBodyAsync(string emailId);
        Task<string> GetEncryptedBodyAsync(string emailId);
        Task GmailSync();
    }
}