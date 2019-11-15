using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.Services.dto_Models;
using EMS.Services.Factories;
using EMS.Services.Factories.Contracts;
using EMS.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace EMS.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly SystemDataContext _context;
        private readonly IApplicationFactory _factory;

        public ApplicationService(SystemDataContext context, IApplicationFactory factory)
        {
            _context = context;
            _factory = factory;
        }       

        public async Task<ApplicationDto> GetAppByMailIdAsync(string emailId)
        {
            var appDomain = await _context.Applications                              
                .FirstOrDefaultAsync(ap => ap.EmailId.ToString() == emailId)
                .ConfigureAwait(false);

            return appDomain.MapToDtoModel();
        }
        public async Task CreateApplicationAsync(string emailId, string userId, string EGN, string name, string phoneNum)
        {
            var factory = _factory.Create(emailId, userId, EGN, name, phoneNum);

            _context.Applications.Add(factory);
         
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }        

        public async Task<List<ApplicationDto>> GetAllAppsAsync()
        {
            var appsDomain = await _context.Applications
                .Include(app => app.Email)
                .Include(app => app.User)
                
                .ToListAsync().ConfigureAwait(false);

            var appsDto = new List<ApplicationDto>();
            foreach (var app in appsDomain)
            {
                appsDto.Add(app.MapToDtoModel());
            }

            return appsDto;
        }
        public async Task ChangeStatusAsync(string applictionId, ApplicationStatus newStatus)
        {
            var email = await _context.Applications
                .FirstOrDefaultAsync(ap => ap.Id.ToString() == applictionId)
                .ConfigureAwait(false);

            email.Status = newStatus;

             await _context.SaveChangesAsync();
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
