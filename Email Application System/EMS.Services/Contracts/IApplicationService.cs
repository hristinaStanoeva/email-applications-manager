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
        Task ChangeStatusAsync(string applictionId, ApplicationStatus newStatus);

        Task<ApplicationDto> FindAsync(string Id);
        Task<List<ApplicationDto>> FindAllApplicationOfUserAsync(string userId);
        Task<List<ApplicationDto>> GetAllAppsAsync();
        Task CreateAsync(string emailId, string userId, string EGN, string name, string phoneNum);
        Task<ApplicationDto> GetByMailIdAsync(string emailId);
        Task Delete(string appId);
        Task<List<ApplicationDto>> GetOpenAppsAsync();
        Task<List<ApplicationDto>> GetClosedAppsAsync();
        Task<string> GetOperatorUsernameAsync(string emailId);
    }
}
