using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Services;
using EMS.Services.Contracts;
using EMS.Services.Factories.Contracts;
using EMS.Services.Tests;
using GmailAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ServiceTests.ApplicationSerivceTests
{
    [TestClass]
    class CreateAsync_Should
    {
        [TestMethod]
        public async Task Create()
        {
            TestUtils.GetContextWithApplications(nameof(Create));

            var gmailServiceMock = new Mock<IGmailAPIService>();
            var appFactoryMock = new Mock<IApplicationFactory>();
            var userSericeMock = new Mock<IUserService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(Create))))
            {
                var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

                userSericeMock
                    .Setup(u => u.GetUserIdAsync("mockUsername"))
                    .ReturnsAsync("mockId");

                appFactoryMock
                    .Setup(f => f.Create("mockEmailId", "mockUserId", "11111111", "mockName", "+35991111"))
                    .Returns(new ApplicationDomain());




            }
        }
    }
}
