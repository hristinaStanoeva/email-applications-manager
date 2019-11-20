using System.Threading.Tasks;

namespace GmailAPI
{
    public interface IGmailAPIService
    {
        Task<string> GetEmailBodyAsync(string emailId);
        Task GmailSync();
    }
}