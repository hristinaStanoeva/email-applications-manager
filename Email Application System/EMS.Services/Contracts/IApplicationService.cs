using EMS.Data.Enums;
using EMS.Services.dto_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Services.Contracts
{
    public interface IApplicationService
    {        
        Task ChangeStatusAsync(string applictionId, ApplicationStatus newStatus);
        Task CreateAsync(string emailId, string userId, string EGN, string name, string phoneNum);
        Task<ApplicationDto> GetByMailIdAsync(string emailId);
        Task Delete(string appId);
        Task<List<ApplicationDto>> GetOpenAppsAsync();
        Task<string> GetOperatorUsernameAsync(string emailId);
        Task<string> GetEmailId(string appId);
        Task<string> GetAppStatus(string mailId);
    }
}
