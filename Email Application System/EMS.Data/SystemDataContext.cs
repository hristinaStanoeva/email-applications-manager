using EMS.Data.dbo_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS.Data
{
    public class SystemDataContext : IdentityDbContext<DboUser>
    {
        public SystemDataContext(DbContextOptions<SystemDataContext> options) : base(options)
        { }

       public DbSet<DboUser> Users { get; set; }
       public DbSet<DboApplication> Applications { get; set; }
       


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
