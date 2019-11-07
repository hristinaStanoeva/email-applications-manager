using EMS.Data.dbo_Models;
using EMS.Services.Factories.Contracts;
using System;

namespace EMS.Services.Factories
{
    public class ApplicationFactory : IApplicationFactory
    {
        public ApplicationDomain CreateApplication(Guid emailId, string egn, string name, string phoneNumber, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
