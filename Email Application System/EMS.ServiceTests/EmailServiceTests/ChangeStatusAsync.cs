using EMS.Data;
using EMS.Data.Enums;
using EMS.Services;
using EMS.Services.Tests;
using GmailAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ServiceTests.EmailServiceTests
{
    [TestClass]
    public class ChangeStatusAsync
    {
        [TestMethod]
        public async Task ChangeStatus()
        {
            TestUtils.GetContextWithEmails(nameof(ChangeStatus));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(ChangeStatus))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var id = TestUtils.Emails[0].Id;                

                await sut.ChangeStatusAsync(id.ToString(), EmailStatus.Open);

                var email = assertContext.Emails.FirstOrDefault(mail => mail.Id == id);
                
                Assert.IsTrue(email.Status == EmailStatus.Open);
            }
        }
    }
}
