using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using EMS.Services.dto_Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Services.Contracts
{
    public interface IApplicationService
    {
        Task<ApplicationDto> CreateAsync(Guid emailId, string egn, string name, string phoneNumber, string userId);
        Task ChangeStatusAsync(string applictionId, ApplicationStatus newStatus);
        Task<ApplicationDto> FindApplicationAsync(string Id);
        Task<List<ApplicationDto>> FindAllApplicationOfUserAsync(string userId);
        List<ApplicationDomain> GetAllAppsAsync();
        Task CreateApplicationAsync(string emailId, string userId, string EGN, string name, string phoneNum);
        Task<ApplicationDto> GetAppByMailIdAsync(string emailId);
    }
}
