using EMS.Data.dbo_Models;
using EMS.Services.Factories.Contracts;
using System;

namespace EMS.Services.Factories
{
    public class AttachmentFactory : IAttachmentFactory
    {
        public DboAttachment CreateAttachment(string name, double sizeMb, Guid emailId)
        {
            throw new NotImplementedException();
        }
    }
}
