using EMS.Data;
using GmailAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Services.Tests.EmailServiceTests
{
    [TestClass]
    public class GetAllEmailsAsync_Should
    {
        [TestMethod]
        public async Task GetAllEmails()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(GetAllEmails));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetAllEmails))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var allEmails = await sut.GetAllEmailsAsync();

                Assert.AreEqual(TestUtils.Emails.Count, allEmails.Count);
            }
        }

        [TestMethod]
        public async Task GetAllAttachments()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(GetAllAttachments));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetAllAttachments))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var allEmails = await sut.GetAllEmailsAsync();

                int actualAttachmentsCount = 0;
                allEmails.ForEach(email =>
                actualAttachmentsCount += email.Attachments.Count);

                int expectedAttachmentsCount = 0;
                TestUtils.Emails.ForEach(email =>
                expectedAttachmentsCount += email.Attachments.Count);

                Assert.AreEqual(expectedAttachmentsCount, actualAttachmentsCount);
            }
        }

        [TestMethod]
        public async Task MapCorrectly_EmailId()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(MapCorrectly_EmailId));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapCorrectly_EmailId))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var allEmails = await sut.GetAllEmailsAsync();
                var expectedEmails = TestUtils.Emails;

                Assert.IsTrue(allEmails.Any(email =>
                email.Id == TestUtils.Emails[0].Id));
                Assert.IsTrue(allEmails.Any(email =>
                email.Id == TestUtils.Emails[1].Id));
                Assert.IsTrue(allEmails.Any(email =>
                email.Id == TestUtils.Emails[2].Id));
            }
        }

        [TestMethod]
        public async Task MapCorrectly_EmailSubject()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(MapCorrectly_EmailSubject));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapCorrectly_EmailSubject))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var allEmails = await sut.GetAllEmailsAsync();
                var expectedEmails = TestUtils.Emails;

                Assert.IsTrue(allEmails.Any(email =>
                email.Subject == TestUtils.Emails[0].Subject));
                Assert.IsTrue(allEmails.Any(email =>
                email.Subject == TestUtils.Emails[1].Subject));
                Assert.IsTrue(allEmails.Any(email =>
                email.Subject == TestUtils.Emails[2].Subject));
            }
        }

        [TestMethod]
        public async Task MapCorrectly_SenderEmail()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(MapCorrectly_SenderEmail));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapCorrectly_SenderEmail))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var allEmails = await sut.GetAllEmailsAsync();
                var expectedEmails = TestUtils.Emails;

                Assert.IsTrue(allEmails.Any(email =>
                email.SenderEmail == TestUtils.Emails[0].SenderEmail));
                Assert.IsTrue(allEmails.Any(email =>
                email.SenderEmail == TestUtils.Emails[1].SenderEmail));
                Assert.IsTrue(allEmails.Any(email =>
                email.SenderEmail == TestUtils.Emails[2].SenderEmail));
            }
        }
    }
}
