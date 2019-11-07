using EMS.Data.dbo_Models;
using EMS.Services.Factories.Contracts;
using System;

namespace EMS.Services.Factories
{
    public class UserFactory : IUserFactory
    {
        public DboUser CreateManager(string username, string email, string password)
        {
            throw new NotImplementedException();
        }

        public DboUser CreateOperator(string username, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
