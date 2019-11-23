using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using EMS.Services.Factories.Contracts;
using EMS.Services.Security;
using System;

namespace EMS.Services.Factories
{
    public class ApplicationFactory : IApplicationFactory
    {
        public ApplicationDomain Create(string emailId, string userId, string EGN, string name, string phoneNum)
        {
            return new ApplicationDomain
            {
                EmailId = Guid.Parse(emailId),
                UserId = userId,
                EGN = Encrypt.EncryptData(EGN),
                Name = Encrypt.EncryptData(name),
                PhoneNumber = Encrypt.EncryptData(phoneNum),
                Status = ApplicationStatus.NotReviewed
            };
        }
    }
}
