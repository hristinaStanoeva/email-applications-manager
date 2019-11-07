using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.Services.dto_Models;

namespace EMS.Services
{
    public class ApplicationService : IApplicationService
    {
        public Task ChangeStatusAsync(string applictionId, ApplicationStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public Task<DtoApplication> CreateAsync(Guid emailId, string egn, string name, string phoneNumber, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<DtoApplication>> FindAllApplicationOfUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<DtoApplication> FindApplicationAsync(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
