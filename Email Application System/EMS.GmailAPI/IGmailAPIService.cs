using System.Threading.Tasks;

namespace GmailAPI
{
    public interface IGmailAPIService
    {
        Task<string> GetEmailBody(string emailId);
        Task GmailSync();
    }
}