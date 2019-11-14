using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using EMS.Services.Factories.Contracts;
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
                EGN = EGN,
                Name = name,
                PhoneNumber = phoneNum,
                Status = ApplicationStatus.NotReviewed
            };
        }
    }
}
