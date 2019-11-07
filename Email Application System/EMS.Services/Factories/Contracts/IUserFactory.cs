using EMS.Data.dbo_Models;

namespace EMS.Services.Factories.Contracts
{
    public interface IUserFactory
    {
        UserDomain CreateManager(string username, string email, string password);
        UserDomain CreateOperator(string username, string email, string password);
    }
}
