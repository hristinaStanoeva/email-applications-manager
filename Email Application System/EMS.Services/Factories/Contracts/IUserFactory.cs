using System.Threading.Tasks;

namespace EMS.Services.Factories.Contracts
{
    public interface IUserFactory
    {
        Task CreateUser(string username, string password, string role);
    }
}
