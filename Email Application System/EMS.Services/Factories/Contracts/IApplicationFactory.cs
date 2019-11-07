using EMS.Data.dbo_Models;
using System;

namespace EMS.Services.Factories.Contracts
{
    public interface IApplicationFactory
    {
        DboApplication CreateApplication(Guid emailId, string egn, string name, string phoneNumber, string userId);
    }
}
