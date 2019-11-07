using EMS.Data.dbo_Models;

namespace EMS.Services.Factories.Contracts
{
    public interface IUserFactory
    {
        DboUser CreateManager(string username, string email, string password);
        DboUser CreateOperator(string username, string email, string password);
    }
}
