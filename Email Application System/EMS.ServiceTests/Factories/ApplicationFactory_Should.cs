using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Services;
using EMS.Services.Contracts;
using EMS.Services.Factories;
using EMS.Services.Factories.Contracts;
using EMS.Services.Tests;
using GmailAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ServiceTests.Factories
{
    [TestClass]
    public class ApplicationFactory_Should
    {
        [TestMethod]
        public async Task CreateApplication()
        {
            TestUtils.GetContextWithApplications(nameof(CreateApplication));
            var emailId = Guid.NewGuid();
            
            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(CreateApplication))))
            {                
                var sut = new ApplicationFactory();

                var application = sut.Create(emailId.ToString(), "userId", "1111", "testName", "+1111");

                Assert.AreEqual(emailId, application.EmailId);
            }
        }
    }
}
