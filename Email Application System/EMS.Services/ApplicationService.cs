using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.Services.dto_Models;

namespace EMS.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly SystemDataContext _context;

        public ApplicationService(SystemDataContext context)
        {
            _context = context;
        }       

        public async Task MarkOpenAsync(string emailId, string userId, string EGN, string name, string phoneNum)
        {
            _context.Applications.Add(new ApplicationDomain
            {
                EmailId = Guid.Parse(emailId),
                UserId = userId,
                EGN = EGN,
                Name = name,
                PhoneNumber = phoneNum,
                Status = ApplicationStatus.NotReviewed
            });

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public List<ApplicationDomain> GetAllAppsAsync()
        {
            return _context.Applications.ToList();
        }
        public Task ChangeStatusAsync(string applictionId, ApplicationStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationDto> CreateAsync(Guid emailId, string egn, string name, string phoneNumber, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ApplicationDto>> FindAllApplicationOfUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationDto> FindApplicationAsync(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
