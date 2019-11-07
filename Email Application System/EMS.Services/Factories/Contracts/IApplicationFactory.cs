using EMS.Data.dbo_Models;
using System;

namespace EMS.Services.Factories.Contracts
{
    public interface IApplicationFactory
    {
        ApplicationDomain CreateApplication(Guid emailId, string egn, string name, string phoneNumber, string userId);
    }
}
