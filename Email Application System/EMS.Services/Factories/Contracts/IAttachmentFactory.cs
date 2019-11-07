using EMS.Data.dbo_Models;
using System;

namespace EMS.Services.Factories.Contracts
{
    public interface IAttachmentFactory
    {
        AttachmentDomain CreateAttachment(string name, double sizeMb, Guid emailId);
    }
}
