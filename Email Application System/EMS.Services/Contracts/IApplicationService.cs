using EMS.Data.Enums;
using EMS.Services.dto_Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Services.Contracts
{
    public interface IApplicationService
    {
        Task<DtoApplication> CreateAsync(Guid emailId, string egn, string name, string phoneNumber, string userId);
        Task ChangeStatusAsync(string applictionId, ApplicationStatus newStatus);
        Task<DtoApplication> FindApplicationAsync(string Id);
        Task<List<DtoApplication>> FindAllApplicationOfUserAsync(string userId);
    }
}
