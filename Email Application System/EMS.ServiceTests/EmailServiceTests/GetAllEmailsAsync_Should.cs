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
            TestUtils.GetContextWithEmails(nameof(GetAllEmails));

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
            TestUtils.GetContextWithEmails(nameof(GetAllAttachments));

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
        public async Task MapsCorrectly_EmailId()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(MapsCorrectly_EmailId));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_EmailId))))
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
        public async Task MapsCorrectly_SenderName()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(MapsCorrectly_SenderName));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_SenderName))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var allEmails = await sut.GetAllEmailsAsync();
                var expectedEmails = TestUtils.Emails;

                Assert.IsTrue(allEmails.Any(email =>
                email.SenderName == TestUtils.Emails[0].SenderName));
                Assert.IsTrue(allEmails.Any(email =>
                email.SenderName == TestUtils.Emails[1].SenderName));
                Assert.IsTrue(allEmails.Any(email =>
                email.SenderName == TestUtils.Emails[2].SenderName));
            }
        }

        [TestMethod]
        public async Task MapsCorrectly_SenderEmail()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(MapsCorrectly_SenderEmail));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_SenderEmail))))
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

        [TestMethod]
        public async Task MapsCorrectly_DateReceived()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(MapsCorrectly_DateReceived));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_DateReceived))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var allEmails = await sut.GetAllEmailsAsync();
                var expectedEmails = TestUtils.Emails;

                Assert.IsTrue(allEmails.Any(email =>
                email.Received == TestUtils.Emails[0].Received));
                Assert.IsTrue(allEmails.Any(email =>
                email.Received == TestUtils.Emails[1].Received));
                Assert.IsTrue(allEmails.Any(email =>
                email.Received == TestUtils.Emails[2].Received));
            }
        }

        [TestMethod]
        public async Task MapsCorrectly_NumberOfAttachments()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(MapsCorrectly_NumberOfAttachments));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_NumberOfAttachments))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var allEmails = await sut.GetAllEmailsAsync();
                var expectedEmails = TestUtils.Emails;

                Assert.IsTrue(allEmails.Any(email =>
                email.NumberOfAttachments == TestUtils.Emails[0].NumberOfAttachments));
                Assert.IsTrue(allEmails.Any(email =>
                email.NumberOfAttachments == TestUtils.Emails[1].NumberOfAttachments));
                Assert.IsTrue(allEmails.Any(email =>
                email.NumberOfAttachments == TestUtils.Emails[2].NumberOfAttachments));
            }
        }

        [TestMethod]
        public async Task MapsCorrectly_Status()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(MapsCorrectly_Status));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_Status))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var allEmails = await sut.GetAllEmailsAsync();
                var expectedEmails = TestUtils.Emails;

                Assert.IsTrue(allEmails.Any(email =>
                email.Status == TestUtils.Emails[0].Status));
                Assert.IsTrue(allEmails.Any(email =>
                email.Status == TestUtils.Emails[1].Status));
                Assert.IsTrue(allEmails.Any(email =>
                email.Status == TestUtils.Emails[2].Status));
            }
        }

        [TestMethod]
        public async Task MapsCorrectly_Subject()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(MapsCorrectly_Subject));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_Subject))))
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
        public async Task MapsCorrectly_Body()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(MapsCorrectly_Body));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_Body))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var allEmails = await sut.GetAllEmailsAsync();
                var expectedEmails = TestUtils.Emails;

                Assert.IsTrue(allEmails.Any(email =>
                email.Body == TestUtils.Emails[0].Body));
                Assert.IsTrue(allEmails.Any(email =>
                email.Body == TestUtils.Emails[1].Body));
                Assert.IsTrue(allEmails.Any(email =>
                email.Body == TestUtils.Emails[2].Body));
            }
        }

        [TestMethod]
        public async Task MapsCorrectly_ToCurrentStatus()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(MapsCorrectly_ToCurrentStatus));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_ToCurrentStatus))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var allEmails = await sut.GetAllEmailsAsync();
                var expectedEmails = TestUtils.Emails;

                Assert.IsTrue(allEmails.Any(email =>
                email.ToCurrentStatus == TestUtils.Emails[0].ToCurrentStatus));
                Assert.IsTrue(allEmails.Any(email =>
                email.ToCurrentStatus == TestUtils.Emails[1].ToCurrentStatus));
                Assert.IsTrue(allEmails.Any(email =>
                email.ToCurrentStatus == TestUtils.Emails[2].ToCurrentStatus));
            }
        }

        [TestMethod]
        public async Task MapsCorrectly_gmailId()
        {
            //Prepare database
            TestUtils.GetContextWithEmails(nameof(MapsCorrectly_gmailId));

            //Prepare dependencies
            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_gmailId))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var allEmails = await sut.GetAllEmailsAsync();
                var expectedEmails = TestUtils.Emails;

                Assert.IsTrue(allEmails.Any(email =>
                email.GmailMessageId == TestUtils.Emails[0].GmailMessageId));
                Assert.IsTrue(allEmails.Any(email =>
                email.GmailMessageId == TestUtils.Emails[1].GmailMessageId));
                Assert.IsTrue(allEmails.Any(email =>
                email.GmailMessageId == TestUtils.Emails[2].GmailMessageId));
            }
        }
    }
}
