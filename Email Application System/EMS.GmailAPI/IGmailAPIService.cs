using System.Threading.Tasks;

namespace GmailAPI
{
    public interface IGmailAPIService
    {
        Task GetEmailBody(string emailId);
        Task GmailSync();
    }
}