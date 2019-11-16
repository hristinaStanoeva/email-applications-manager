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

        public async Task Delete(string appId)
        {
            var application = await _context.Applications.FirstOrDefaultAsync(app => app.Id.ToString() == appId)
                .ConfigureAwait(false);

            _context.Applications.Remove(application);

            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationDto> GetByMailIdAsync(string emailId)
        {
            var appDomain = await _context.Applications 
                .Include(app => app.Email)
                .Include(app => app.User)
                .FirstOrDefaultAsync(app => app.Email.Id.ToString() == emailId)
                .ConfigureAwait(false);

            return appDomain.MapToDtoModel();
        }
        public async Task CreateAsync(string emailId, string userId, string EGN, string name, string phoneNum)
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

        public async Task<List<ApplicationDto>> GetOpenAppsAsync()
        {
            var appsDomain = await _context.Applications
                .Where(app => app.Status == ApplicationStatus.NotReviewed)
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
        
        public Task<List<ApplicationDto>> FindAllApplicationOfUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationDto> FindAsync(string Id)
        {
            var appDbo = await _context.Applications
                .FirstOrDefaultAsync(app => app.Id.ToString() == Id)
                .ConfigureAwait(false);

            return appDbo.MapToDtoModel();
        }
    }
}
